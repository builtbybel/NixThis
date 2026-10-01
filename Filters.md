# Writing filters

`Data\Filters.ini` is everything NixThis knows. Swap the file and it knows different things, with
no new build. The key reference lives at the top of the ini itself. This page is about the part a
reference cannot teach: how you get from a thing on screen to a working section.

## What a section is

One section is one question. Most sections say which element on screen they belong to and which
registry value turns that element off:

```ini
[Task view button]
Where=Taskbar
Match=taskviewbutton
Process=explorer
App=Taskbar
Question=Remove the Task view button from the taskbar?
Detail=The button next to Search disappears. Win+Tab still works.
Path=HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced
ValueName=ShowTaskViewButton
ValueType=DWORD
RecommendedValue=0
DefaultValue=1
Restart=explorer
```

The section name is the identity of the filter. The history file stores it, undo finds its rule
again by it, and the locale files hang their translations off it as `Rule_Name_Task view button`.
Renaming a section orphans both.

Only a section without `Action` becomes a row in the list. `group`, `scan`, `uninstall` and `apps`
do other jobs, described below.

## From a sighting to a section

Point the picker at something NixThis does not know yet. Nothing happens on screen, but a line is
appended to `Data\Sightings.log` next to the exe. That line is the raw material:

```
2026-10-01T23:37:16	explorer	1 | syslistview32 | progman | #32769	taskslinger-v0.9.2-windows-x64 | desktop | program manager
```

Four columns, tab separated: time, process, ids, labels. The ids and labels are not only the
element you hit, they are the element plus four levels of parents, which is why there are several.

* The **ids** are `AutomationId` and `ClassName`. The same on every Windows, in every language.
  Pick the most specific one that still belongs to the thing you mean and put it in `Match`.
* The **labels** are what the user reads, so they differ per language. Use them only when the ids
  carry nothing usable, which is the case above: `1`, `syslistview32`, `progman` and `#32769`
  describe the desktop, not the icon on it. Then the label goes into `Names`.
* The **process** goes into `Process` when you need to narrow the match down.

Everything is compared in lower case, and spaces and dashes are dropped on both sides, so
`Match=Task View` finds `TaskViewButton`. Write the ini however it reads best.

## How a match is decided

For each section, in this order:

1. `Process` must be contained in the sighting's process, or be empty. This is an **and**: a
   section with a `Process` can never match an element drawn by another program.
2. Then one of `Match` or `Names` has to hit. This is an **or**, and either one is enough.
3. Of all sections that fit, the one with the most `Match` words wins, so a specific section beats
   a broad one.
4. `Action=group` sections never answer. They only name an app.

Two consequences worth knowing before you debug:

* An element with no id of its own and a label that belongs to a different process than the window
  cannot have a `Process`. The desktop icon above is drawn by explorer, its own window is not.
  A filled `Process` would have to be true for both.
* If a section does not fire, compare the exact string in the log against the exact string in the
  ini, character by character, before looking anywhere else. A product spelled `TaskSlingr` and a
  file called `taskslinger` are not the same word.

## When the element is a miss

Not every element belongs to a filter. Two keys catch the rest.

`App` names the programs a section is about. It never decides a match. It is what NixThis falls
back to: point at anything called Microsoft Edge, or at anything inside `msedge`, and the search
box is filled with Edge instead of a card being shown.

`Action=group` exists because the process often says nothing. Explorer draws the taskbar, the file
windows and the desktop alike, so only the window class tells them apart:

```ini
[Taskbar]
Action=group
Match=shell_traywnd
App=Taskbar
```

That section is not a filter and never appears in the list. It only says that whatever sits inside
a `Shell_TrayWnd` belongs to the taskbar.

## The other actions

`Action=scan` means pointing at it opens the list instead of a card. The Start button uses it.

`Action=uninstall` means the section name is the app as the Start menu shows it, and there is no
registry value. Undo cannot write an app back, so the card says so and the Store page is offered.

```ini
[Copilot]
Names=Copilot
Process=explorer
Action=uninstall
Question=Remove Copilot?
Detail=The app is uninstalled and its taskbar icon goes with it. The Store has it back any time.
```

`Action=apps` sections are package lists at the bottom of the file, one `Package` line per entry.
They produce no rows either. They only decide how NixThis talks about an app that is installed:
`Safety=Protected` greys the tick out, `Safety=Safe` recommends it, and a package on no list is
offered without a recommendation, because what is clutter to one person is why someone else turns
the PC on. A line starting with `*` matches the end of the package name instead of the start,
which is how a whole publisher is caught.

## The registry half

`Path` accepts `HKEY_LOCAL_MACHINE`, `HKEY_CLASSES_ROOT` and `HKEY_CURRENT_USER`. Anything else is
read as HKCU. Machine wide keys and everything under `Policies` need an administrator, and a
refused write is reported by name rather than swallowed, so the row keeps its old tick.

`ValueType` is `DWORD` or `String`. A DWORD value has to parse as a number.

`DefaultValue` is what undo writes back when NixThis has no older value of its own. Use
`<deletevalue>` when Windows has no default, which is true of most policy values: they only exist
once somebody sets them, and writing a zero over them is not the same state as removing them.

`Restart=explorer` is needed by most taskbar and Explorer settings, and it is expensive: on more
than one monitor it throws the desktop icons back onto the first screen. `Restart=desktop` only
tells the shell to draw the desktop icons again, which is enough for a desktop icon.

## Texts and translation

`Question` and `Detail` are written in English in the ini. A locale file overrides them per section
through `Rule_Question_<section>` and `Rule_Detail_<section>`, the section name itself through
`Rule_Name_<section>`, and `Names` through `Rule_Names_<section>`, which is what makes label
matching work on a German Windows. A missing key falls back to the ini, so a new section works
untranslated.

`Where` needs a `Where_<value>` line in the locale files. The existing values are Taskbar,
StartMenu, Explorer, Desktop, Search, LockScreen and Edge. Omitting `Where` means Windows itself.

## Three real cases

All three log lines below are real, taken from a German Windows 11, which is why the labels are
German and the ids are not.

### A button that carries its own id

```
explorer	titlebar | cabinetwclass | #32769	net48 - datei-explorer
```

The pointer was on the title bar of a file window. `titlebar` is too generic to own a section, but
`cabinetwclass` is the file window itself, and that is exactly what a group is for:

```ini
[File Explorer]
Action=group
Match=cabinetwclass
App=Explorer
```

No card appears here. NixThis says Explorer, fills the search box with it, and the list shows every
Explorer filter at once. That is the normal answer for a window as a whole. Only the parts inside
it get sections of their own.

### A pinned taskbar button

```
explorer	appid: c:\users\belim\...\nixthis.exe | taskbar.tasklistbuttonautomationpeer |
taskbarframe | ... | shell_traywnd | #32769	nixthis angeheftet
```

Three things are readable at once, and they answer in this order. The `appid:` is the app the
button stands for, so a pinned app is recognised as that app even though explorer drew it.
`taskbar.tasklistbuttonautomationpeer` says it is a pinned button rather than a shell button.
`shell_traywnd` is the taskbar, the `[Taskbar]` group, which is the last and broadest answer.

This is why `Match` is ordered by length: a section for the button beats the group for the window
it sits in, and nothing had to be written down twice.

### A thing with no id at all

```
explorer	1 | syslistview32 | progman | #32769	taskslinger-v0.9.2-windows-x64 | desktop | program manager
```

A desktop icon. The ids describe the desktop, not the icon: `1` is the item index, `syslistview32`
the icon view, `progman` the desktop. Nothing there names what was clicked. Only the first label
does, so the section has to match on `Names`.

It also must not set `Process`. The icon is drawn by explorer, the program it starts is not, and a
`Process` has to be true for every element the section is meant to catch. Compare with the window
of the same program, which does carry an id:

```
taskslinger-v0.9.2-windows-x64	titlebar | taskslinger | #32769	taskslinger - 27.09.2026 07:11:51
```

So one section can cover both, the id for the window and the label for the icon:

```ini
[Example]
Match=taskslinger
Names=taskslinger
```

And this is the mistake to expect: the product is written TaskSlingr, the file is called
taskslinger. `Names=TaskSlingr` matches nothing, and no amount of restarting changes that. When a
section does not fire, read the log line and the ini side by side first.

## Testing a new section

The ini is read once at start. Edit it and restart NixThis, otherwise you are testing the old
file. Also mind which copy you are editing: the running app reads the `Data` folder next to its
own exe, which during development is `bin\Debug\net48\Data`.

When a section is meant to be thrown away afterwards, point it at a key of your own, for example
`HKEY_CURRENT_USER\Software\NixThis\Test`. Then the whole chain, picker to card to history to undo,
can be walked through without anything in Windows changing.
