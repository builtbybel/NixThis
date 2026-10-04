# Translating NixThis

One file per language in `Localization`, named after the language code. Copy `en.json`, translate
the values, leave the keys untouched. `es.json` covers es-ES, es-MX and every other Spanish
variant, because NixThis cuts the code at the dash and looks for the neutral one as well.

English is loaded first and your file is laid over it, so a key you have not translated yet shows
the English text instead of an empty label. An unfinished file is safe to ship. There is one
exception, and it is the only part of this page you really have to read: the nine `Rule_Names_`
keys at the bottom.

## The three keys at the top

`_Language` is the name of your language written in your language. It is what the language list in
the settings shows, so "Español" and not "Spanish".

`_Translator` and `_TranslatorUrl` are your name and a link to you. Both are optional. The about
page simply says nothing about the translation when they are empty.

## {app}

`{app}` stands for the product name and is replaced when the text is shown. Keep it where it is.

## The nine keys nobody reads

Everything else in the file is a text for the user. `Rule_Names_` is not. Those are the words
NixThis looks for **on the screen** when an element carries no technical id of its own, and they
have to be the exact wording your Windows shows. They are also read from the language of Windows
rather than the language chosen in the settings, because they describe what is on screen and not
what the user reads in the app.

Nine filters work this way and eight of them have nothing else to fall back on:

| Key | English | What to write |
|---|---|---|
| `Rule_Names_Copilot` | Copilot | the name in the Start menu |
| `Rule_Names_Explorer OneDrive banner` | OneDrive | the banner's wording |
| `Rule_Names_Phone panel in Start` | Phone Link;Phone | both spellings, separated by `;` |
| `Rule_Names_Gallery in Explorer` | Gallery | the entry in Explorer's sidebar |
| `Rule_Names_Explorer opens Home` | Home | the entry in Explorer's sidebar |
| `Rule_Names_Spotlight icon on the desktop` | Learn about this picture | the desktop icon's label |
| `Rule_Names_Recycle Bin on the desktop` | Recycle Bin | the desktop icon's label |
| `Rule_Names_Show more options` | Show more options | the entry in the right click menu |
| `Rule_Names_Edge sidebar` | Copilot;Discover;Sidebar | all three, separated by `;` |

Several words for one filter are written with `;` between them. Case does not matter, and spaces
are ignored, so "Papelera de reciclaje" and "papelerade reciclaje" find the same thing.

Skip these keys and those filters stop recognising anything. Nothing breaks and no error appears,
the user simply points at the Recycle Bin and NixThis says it does not know it.

## Before you send the file

It has to be valid JSON in UTF-8, otherwise NixThis loads nothing from it and falls back to
English everywhere. One line is enough to be sure:

```
python -c "import json; json.load(open('Localization/es.json', encoding='utf-8'))"
```

No output means the file is fine.
