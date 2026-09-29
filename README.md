# NixThis
what uBlock does for the browser, but for windows itself.

the idea came from setting up yet another fresh pc. advertising id on, bing in the search box,
chat button in the taskbar, "here's what's new after your update" in fullscreen. in a browser i've
been clicking that stuff away for years. point at the thing, thing is gone, and i never had to
know what it's actually called.

windows had nothing like that. it had debloaters. 300 checkboxes, no idea which one does what,
and afterwards either the ads are gone or the store is.

so: eyedropper on, point at the junk, nix this.

## how it works

**pointing.** global mouse hook plus ui automation. whatever sits under the cursor gets read with
`FromPoint`, a borderless window draws a frame around it, and the click gets swallowed before it
reaches the button it landed on. the windows 11 taskbar only ever returns itself from `FromPoint`,
so from there the tree gets walked down by hand until the real element shows up.

**recognizing.** from that element and up to four of its parents it collects `AutomationId`,
`ClassName` and `Name`, plus the process. that's the signature that gets matched against the
filter list. visible text counts last, because that one is different in every windows language.

**turning it off.** a match is a registry value in the end. nothing gets deleted, nothing gets
patched by script, no services get killed. the stuff that can't be done through the registry
(preinstalled apps) goes through powershell uninstall, and a small protected list makes sure you
can't nuke the store or the runtime by accident.

**taking it back.** every change goes into `Data\History.tsv` together with its old value. the
history is just a list with an undo button. it's also the only number the window shows as
"removed" - meaning the stuff this thing actually touched, not some marketing number.

**nothing matched?** then the signature gets written to `Data\Sightings.log`. that's the raw
material for new filters.

## the filter list

everything it knows lives in `Data\Filters.ini`. one section per filter, with path, value name,
recommended value and the default to write back on undo. swap the file, it knows different things.
no rebuild, no installer. the list has its own version number, shown next to the filter count.

## building

.net framework 4.8, winforms, no dependencies.

some filters live under HKLM or under `\Policies\`. those change the whole machine, they get
refused for a normal user, and it offers to restart itself as admin.

there's also an explain button per row that asks an llm what a setting actually does. needs your
own api key, and without one it's just off.

## state

works, i use it, the filter list keeps growing
