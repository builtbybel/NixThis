# Filter schreiben

`Data\Filters.ini` ist alles, was NixThis weiß. Tauscht man die Datei aus, weiß NixThis andere
Dinge, ohne neuen Build. Die Liste aller Schlüssel steht oben in der ini selbst. Diese Seite
erklärt den Teil, den eine Schlüsselliste nicht erklären kann: wie aus einem Ding auf dem
Bildschirm ein funktionierender Abschnitt wird.

## Was ein Abschnitt ist

Ein Abschnitt ist eine Frage. Die meisten sagen, zu welchem Element auf dem Bildschirm sie gehören
und welcher Registry Wert dieses Element abschaltet:

```ini
[Task view button]
Where=Taskbar
Match=taskviewbutton
Process=explorer
App=Taskbar
Question=Remove the Task View button from the taskbar?
Detail=The button disappears. Win+Tab keeps working.
Path=HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced
ValueName=ShowTaskViewButton
ValueType=DWORD
RecommendedValue=0
DefaultValue=1
```

Der Abschnittsname ist die Identität des Filters. Die Verlaufsdatei speichert ihn, das Zurücknehmen
findet darüber seine Regel wieder, und die Sprachdateien hängen ihre Übersetzungen daran auf, als
`Rule_Name_Task view button`. Wer einen Abschnitt umbenennt, verwaist beides.

Nur ein Abschnitt ohne `Action` wird eine Zeile in der Liste. `group`, `scan`, `uninstall` und
`apps` haben andere Aufgaben, siehe unten.

## Von der Sichtung zum Abschnitt

Zeige mit dem Picker auf etwas, das NixThis noch nicht kennt. Auf dem Bildschirm passiert nichts,
aber in `Data\Sightings.log` neben der exe landet eine Zeile. Die ist das Rohmaterial:

```
2026-10-01T23:37:16	explorer	1 | syslistview32 | progman | #32769	taskslinger-v0.9.2-windows-x64 | desktop | program manager
```

Vier Spalten, durch Tabs getrennt: Zeit, Prozess, Ids, Beschriftungen. Ids und Beschriftungen sind
nicht nur das getroffene Element, sondern das Element plus vier Ebenen darüber. Darum sind es
mehrere.

* Die **Ids** sind `AutomationId` und `ClassName`. Auf jedem Windows gleich, in jeder Sprache.
  Nimm die genaueste, die noch zu dem gemeinten Ding gehört, und schreibe sie in `Match`.
* Die **Beschriftungen** liest der Benutzer, sie unterscheiden sich also je Sprache. Nutze sie nur,
  wenn die Ids nichts Brauchbares hergeben, so wie oben: `1`, `syslistview32`, `progman` und
  `#32769` beschreiben den Desktop, nicht das Symbol darauf. Dann gehört die Beschriftung in
  `Names`.
* Der **Prozess** gehört in `Process`, wenn der Treffer eingegrenzt werden muss.

Verglichen wird alles in Kleinbuchstaben, und auf beiden Seiten fallen Leerzeichen und Bindestriche
weg. `Match=Task View` findet also `TaskViewButton`. Schreibe die ini so, wie sie am besten zu
lesen ist.

## Wie ein Treffer entschieden wird

Für jeden Abschnitt, in dieser Reihenfolge:

1. `Process` muss im Prozess der Sichtung enthalten sein oder leer bleiben. Das ist ein **und**:
   ein Abschnitt mit `Process` kann nie auf ein Element passen, das ein anderes Programm gezeichnet
   hat.
2. Danach muss `Match` oder `Names` treffen. Das ist ein **oder**, eines von beiden genügt.
3. Von allen passenden Abschnitten gewinnt der mit den meisten `Match` Wörtern. Der genauere
   Abschnitt schlägt also den allgemeineren.
4. Abschnitte mit `Action=group` antworten nie. Sie benennen nur eine App.

Zwei Folgen davon sollte man vor der ersten Fehlersuche kennen:

* Ein Element ohne eigene Id, dessen Beschriftung zu einem anderen Prozess gehört als das Fenster,
  darf kein `Process` haben. Das Desktopsymbol oben zeichnet der Explorer, das eigene Fenster des
  Programms nicht. Ein gefülltes `Process` müsste für beide stimmen.
* Wenn ein Abschnitt nicht greift, vergleiche zuerst die Zeichenfolge im Log mit der in der ini,
  Zeichen für Zeichen, bevor du irgendwo anders suchst. Ein Produkt namens `TaskSlingr` und eine
  Datei namens `taskslinger` sind nicht dasselbe Wort.

## Wenn das Element nichts hergibt

Nicht jedes Element gehört zu einem Filter. Zwei Schlüssel fangen den Rest auf.

`App` benennt die Programme, um die ein Abschnitt geht. Es entscheidet nie einen Treffer. Es ist
das, worauf NixThis zurückfällt: zeige auf irgendetwas, das Microsoft Edge heißt, oder auf
irgendetwas in `msedge`, und das Suchfeld wird mit Edge gefüllt, statt dass eine Karte erscheint.

`Action=group` gibt es, weil der Prozess oft nichts sagt. Der Explorer zeichnet die Taskleiste, die
Dateifenster und den Desktop gleichermaßen, nur die Fensterklasse unterscheidet sie:

```ini
[Taskbar]
Action=group
Match=shell_traywnd
App=Taskbar
```

Dieser Abschnitt ist kein Filter und erscheint nie in der Liste. Er sagt nur, dass alles in einem
`Shell_TrayWnd` zur Taskleiste gehört.

## Die anderen Actions

`Action=scan` heißt, dass ein Klick darauf die Liste öffnet statt einer Karte. Der Startknopf nutzt
das.

`Action=uninstall` heißt, dass der Abschnittsname die App ist, so wie das Startmenü sie zeigt, und
dass es keinen Registry Wert gibt. Eine App kann nicht zurückgeschrieben werden, darum sagt die
Karte das und bietet die Store Seite an.

```ini
[Copilot]
Names=Copilot
Process=explorer
Action=uninstall
Question=Remove Copilot?
Detail=The app is uninstalled and its taskbar icon goes with it. The Store has it back any time.
```

`Action=apps` Abschnitte sind Paketlisten am Ende der Datei, eine `Package` Zeile pro Eintrag. Sie
erzeugen ebenfalls keine Zeilen. Sie entscheiden nur, wie NixThis über eine installierte App
spricht: `Safety=Protected` sperrt das Häkchen, `Safety=Safe` empfiehlt sie, und ein Paket auf
keiner Liste wird ohne Empfehlung angeboten, denn was für den einen Müll ist, ist für den anderen
der Grund, den PC einzuschalten. Eine Zeile, die mit `*` beginnt, passt auf das Ende des
Paketnamens statt auf den Anfang. So erwischt man einen ganzen Hersteller.

## Die Registry Hälfte

`Path` versteht `HKEY_LOCAL_MACHINE`, `HKEY_CLASSES_ROOT` und `HKEY_CURRENT_USER`. Alles andere
wird als HKCU gelesen. Maschinenweite Schlüssel und alles unter `Policies` brauchen einen
Administrator. Ein abgelehnter Schreibvorgang wird mit Namen gemeldet und nicht verschluckt, die
Zeile behält also ihr altes Häkchen.

`ValueType` ist `DWORD` oder `String`. Ein DWORD Wert muss sich als Zahl lesen lassen.

`DefaultValue` ist das, was das Zurücknehmen schreibt, wenn NixThis keinen eigenen älteren Wert
hat. Nutze `<deletevalue>`, wenn Windows keinen Standard hat, was auf die meisten Richtlinienwerte
zutrifft: sie existieren erst, wenn jemand sie setzt, und eine Null darüber zu schreiben ist nicht
derselbe Zustand wie sie zu entfernen.

`Restart=explorer` brauchen die meisten Taskleisten und Explorer Einstellungen, und es ist teuer:
auf mehr als einem Monitor wirft es die Desktopsymbole zurück auf den ersten Bildschirm.
`Restart=desktop` lässt die Shell nur die Desktopsymbole neu zeichnen, was für ein Desktopsymbol
genügt.

## Texte und Übersetzung

`Question` und `Detail` stehen auf Englisch in der ini. Eine Sprachdatei überschreibt sie pro
Abschnitt über `Rule_Question_<Abschnitt>` und `Rule_Detail_<Abschnitt>`, den Abschnittsnamen über
`Rule_Name_<Abschnitt>`. Ein fehlender Schlüssel fällt auf die ini zurück, ein neuer Abschnitt
funktioniert also auch unübersetzt.

`Names` wird genauso übersetzt, über `Rule_Names_<Abschnitt>`, aber es wird aus der Sprache
gelesen, die **Windows** anzeigt, und nicht aus der, die in den Einstellungen gewählt ist. Es ist
der eine Text, den niemand liest: er muss der Beschriftung auf dem Bildschirm entsprechen, und die
ändert sich nicht, wenn jemand die App auf Englisch stellt. Was ein Übersetzer darüber wissen muss,
steht in [`Translations.md`](Translations.md).

`Where` braucht eine Zeile `Where_<Wert>` in den Sprachdateien. Es benennt den Ort, an dem ein
Filter sitzt: Taskbar, StartMenu, Explorer, Desktop, Search, LockScreen und Edge. Drei weitere
Werte benennen statt eines Ortes ein Thema, für die Schalter, auf die man nirgends zeigen kann:
Ads, Privacy und Apps. Kein `Where` bedeutet Windows selbst.

Der Ort gewinnt immer gegen das Thema. Die Werbung auf dem Sperrbildschirm ist Werbung, aber man
kann auf sie zeigen, also bleibt sie unter LockScreen. Ohne diese Reihenfolge würde die Spalte Wo
zwei Fragen gleichzeitig beantworten, und der Trichter darüber böte denselben Filter doppelt an.

## Drei echte Fälle

Die drei Logzeilen unten sind echt, von einem deutschen Windows 11. Darum sind die Beschriftungen
deutsch und die Ids nicht.

### Ein Knopf mit eigener Id

```
explorer	titlebar | cabinetwclass | #32769	net48 - datei-explorer
```

Der Zeiger war auf der Titelleiste eines Dateifensters. `titlebar` ist zu allgemein für einen
eigenen Abschnitt, aber `cabinetwclass` ist das Dateifenster selbst, und genau dafür gibt es die
Gruppe:

```ini
[File Explorer]
Action=group
Match=cabinetwclass
App=Explorer
```

Hier erscheint keine Karte. NixThis sagt Explorer, füllt das Suchfeld damit und die Liste zeigt
alle Explorer Filter auf einmal. Das ist die normale Antwort für ein ganzes Fenster. Nur die Teile
darin bekommen eigene Abschnitte.

### Ein angehefteter Taskleistenknopf

```
explorer	appid: c:\users\belim\...\nixthis.exe | taskbar.tasklistbuttonautomationpeer |
taskbarframe | ... | shell_traywnd | #32769	nixthis angeheftet
```

Drei Dinge sind gleichzeitig lesbar, und sie antworten in dieser Reihenfolge. Die `appid:` ist die
App, für die der Knopf steht, eine angeheftete App wird also als diese App erkannt, obwohl der
Explorer sie gezeichnet hat. `taskbar.tasklistbuttonautomationpeer` sagt, dass es ein angehefteter
Knopf ist und kein Knopf der Shell. `shell_traywnd` ist die Taskleiste, die Gruppe `[Taskbar]`, und
damit die letzte und allgemeinste Antwort.

Darum wird nach der Länge von `Match` sortiert: ein Abschnitt für den Knopf schlägt die Gruppe für
das Fenster, in dem er sitzt, und niemand musste etwas zweimal aufschreiben.

### Etwas ganz ohne Id

```
explorer	1 | syslistview32 | progman | #32769	taskslinger-v0.9.2-windows-x64 | desktop | program manager
```

Ein Desktopsymbol. Die Ids beschreiben den Desktop, nicht das Symbol: `1` ist die Position in der
Liste, `syslistview32` die Symbolansicht, `progman` der Desktop. Nichts davon benennt das
Angeklickte. Nur die erste Beschriftung tut das, der Abschnitt muss also über `Names` treffen.

Und er darf kein `Process` setzen. Das Symbol zeichnet der Explorer, das Programm dahinter nicht,
und `Process` muss für jedes Element stimmen, das der Abschnitt fangen soll. Zum Vergleich das
Fenster desselben Programms, das durchaus eine Id hat:

```
taskslinger-v0.9.2-windows-x64	titlebar | taskslinger | #32769	taskslinger - 27.09.2026 07:11:51
```

Ein Abschnitt kann also beides abdecken, die Id für das Fenster und die Beschriftung für das
Symbol:

```ini
[Example]
Match=taskslinger
Names=taskslinger
```

Und das ist der Fehler, mit dem zu rechnen ist: das Produkt schreibt sich TaskSlingr, die Datei
heißt taskslinger. `Names=TaskSlingr` trifft nichts, und kein Neustart ändert daran etwas. Wenn ein
Abschnitt nicht greift, lies zuerst die Logzeile und die ini nebeneinander.

## Einen neuen Abschnitt testen

Die ini wird einmal beim Start gelesen. Nach dem Bearbeiten NixThis neu starten, sonst testet man
die alte Datei. Und achte darauf, welche Kopie du bearbeitest: die laufende App liest den Ordner
`Data` neben ihrer eigenen exe, in der Entwicklung also `bin\Debug\net48\Data`.

Wenn ein Abschnitt danach wieder weg soll, lass ihn auf einen eigenen Schlüssel zeigen, zum
Beispiel `HKEY_CURRENT_USER\Software\NixThis\Test`. Dann kann die ganze Kette, von Picker über
Karte und Verlauf bis zum Zurücknehmen, durchgespielt werden, ohne dass sich in Windows etwas
ändert.
