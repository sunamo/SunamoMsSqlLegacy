---
schema_version: 10
type: library
file_count: 24
avg_lines_per_file: 45
total_lines: 1087
metrics_lm: 2026-10-04 12:00:00
move_to_legacy_percent: 10
description_updated: 2026-10-04
links_updated: 2026-10-04
github_source_url: not found
origin_status: own
origin_checked: not run
article_source_url: not found
article_status: own
article_checked: not run
last_build_ok: yes
last_build_date: 2026-10-04
last_tests_run_date: not run
covered_lines: not run
---

## Description

Starší vrstva nad Microsoft SQL Serverem. Obsahuje definice sloupců tabulek, generátor příkazu CREATE TABLE a pomocné metody pro dotazy (select, insert, skalární dotazy). Vznikla přesunem kódu z původního repa sunamo.task do balíčku bez závislostí na jiných balíčcích.

## Původ zdrojáků

Staženo z GitHubu: **ne** — vlastní kód knihovny sunamo.

- Zdroj určen podle: kód pochází z repa sunamo.task (vlastní autor, Azure DevOps).

## Doporučení přesunu do legacy

Doporučení přesunu do sunamocz-legacy.visualstudio.com: **10 %** — nový balíček, starý styl API.

- Starší API se statickými členy.
- Nemá testy.

## Vazby na moje repa

- Submoduly: žádné
- ProjectReference / PackageReference: žádné
