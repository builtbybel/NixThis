<div align="center">

# NixThis

**What uBlock does for websites, but for Windows itself.**

[![Release](https://img.shields.io/github/v/release/builtbybel/NixThis?style=flat-square)](https://github.com/builtbybel/NixThis/releases)
![Windows](https://img.shields.io/badge/Windows-10%20%7C%2011-0078D4?style=flat-square)

</div>

<img width="1536" height="1024" alt="nixthis-showcase" src="https://github.com/user-attachments/assets/85e5900e-d272-42c1-972b-969d1a0078e2" />


Windows has a habit of adding things nobody asked for. Ads, suggestions, a Chat button, Bing in
Search, another full-screen "here's what's new" page after an update.

I got tired of looking up what Microsoft calls each of these things, finding the right setting,
then doing it all again on the next PC. Most debloaters did not really solve that problem for me
either. They gave me 300 checkboxes and expected me to understand all of them.

So I built NixThis. Point at the annoying thing, and it looks for the setting behind it.

---

## Install

Grab the latest build from the [Releases page](https://github.com/builtbybel/NixThis/releases),
extract it somewhere you can write to, run `NixThis.exe`. That's it, it's portable.

Needs Windows 10 or 11 and .NET Framework 4.8.

---

## Using it

There are two ways, and you can mix them freely.

### Point at something

| | |
|---|---|
| **1** | Click **Pick an element**, or press <kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>Space</kbd> from anywhere |
| **2** | Move the pointer over the thing that annoys you |
| **3** | A **green frame** means NixThis recognises it. Click it |
| **4** | Read what will change, then block it or leave it alone |

The click is caught by NixThis, so it does not accidentally activate the button underneath. Windows
gives a shortcut to whoever asked for it first, so if another app already owns
<kbd>Ctrl</kbd> + <kbd>Shift</kbd> + <kbd>Space</kbd>, the button is the way in.

> [!NOTE]
> A **grey frame** means NixThis does not know that element yet. The sighting is recorded locally
> so it can become a filter later. Nothing is changed.

### Use the filter list

Open it with **More**, and fold it away again with **Less**.

The list shows everything NixThis currently knows about, including things that are not visible on
screen right now. Search it, tick what should go, choose **Apply changes**. Each row tells you where
the item lives and what will actually change.

This is also where installed apps appear. Apps required by Windows are protected. Everything else is
shown as a choice, rather than pretending every preinstalled app is automatically "bloat".

### Changed your mind

Open **What you blocked**. Registry changes remember their previous value, so **Bring it back**
restores exactly what was there before.

> [!IMPORTANT]
> Removed Store apps are the exception. Windows offers no real undo for an uninstall, so NixThis
> opens the matching Microsoft Store page instead.

---

## Under the hood

<details>
<summary><b>What NixThis actually changes</b></summary>

Most filters write one documented or well-known Windows registry value. NixThis does not patch
system files, disable random services, or run a giant mystery script behind an "Optimize" button.

Some machine-wide settings need administrator rights. NixThis first tries the change normally. If
Windows refuses it, the app explains why and can restart itself with administrator rights.

Preinstalled apps are removed through Windows PowerShell. A protected package list prevents core
components such as the Store, App Installer and Windows runtimes from being offered for removal.

</details>

<details>
<summary><b>How the pointing works</b></summary>

The picker combines a global mouse hook with Windows UI Automation. While the pointer moves,
NixThis reads the element under it using `AutomationElement.FromPoint` and draws a border around the
result.

One element is rarely enough to identify a Windows feature reliably, so NixThis also looks at its
parents and collects the `AutomationId`, `ClassName`, visible name and owning process. That signature
is compared with the filter list. Stable IDs are preferred because visible text changes with the
Windows language.

The Windows 11 taskbar needs some extra work. It often reports only the taskbar itself, so NixThis
walks down the automation tree to find the button that is really under the pointer.

</details>

<details>
<summary><b>The filter list</b></summary>

Everything NixThis knows lives in [`Data/Filters.ini`](Data/Filters.ini). Each filter describes how
an element is recognised, what setting sits behind it, what value blocks it and what should be used
to restore it.

The list has its own version number and can grow independently from the app. Pointing at an unknown
element writes its signature to `Data/Sightings.log`. That log stays on the PC and gives me the raw
material for adding new filters.

How a filter is written, from a log line to a working section, is explained in
[`Filters.md`](Filters.md) ([deutsch](Filters.de.md)).

</details>

<details>
<summary><b>Optional AI explanations</b></summary>

The **Explain** button can ask an AI provider for a short second opinion about a filter. It is
optional and stays disabled until you add your own API key in Settings.

Without it, NixThis works completely locally. When you do request an explanation, only the name and
technical details of that one setting are sent, never a scan of your PC.

</details>

---

## Still early

NixThis only blocks things it recognises. Windows changes often, names move around and every new
build seems to invent another place for a suggestion. The filter list will need to keep learning.

If something is not recognised, or a filter no longer works on your build, please
[open an issue](https://github.com/builtbybel/NixThis/issues) and include what you pointed at and
which Windows version you are on. That is much more useful than pretending the first release already
knows everything.

The first release is out, but the source is not published yet. I built the first working version
quickly because I wanted to find out whether this idea was useful outside my own PCs. It is.
I am cleaning up and refactoring the code now, so the repository starts with something I can keep
maintaining instead of a rushed code dump. The source will follow.
