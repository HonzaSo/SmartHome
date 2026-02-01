Projekt: IoT Smart Home Hub
Cílem je vytvořit GraphQL rozhraní pro správu chytré domácnosti, které řeší typické problémy grafových dat (vztahy 1:N a N:M, efektivní načítání).

1. Doménový model
   Navrhni systém, který obsahuje následující entity a vztahy:

Domácnost (Home): Jméno, adresa.

Místnost (Room): Název, typ (kuchyně, ložnice atd.), patří do jedné domácnosti.

Zařízení (Device): Název, výrobce, stav (online/offline), patří do jedné místnosti.

Typ zařízení (DeviceType): Enum (Světlo, Termostat, Zásuvka, Kamera).

2. Požadavky na GraphQL Schema
   A. Queries (Čtení)
   GetHomeDetails: Vrátí detail domácnosti včetně všech místností a v každé místnosti seznam jejích zařízení.

SearchDevices: Vyhledávání zařízení napříč celým systémem podle názvu nebo typu.

RoomInventory: Seznam všech zařízení v konkrétní místnosti s možností stránkování (limit/offset).

B. Mutations (Zápisy)
RegisterDevice: Přidání nového zařízení do konkrétní místnosti. Vstupem musí být InputType.

UpdateDeviceState: Změna stavu zařízení (např. zapnout/vypnout).

MoveDevice: Přesun zařízení z jedné místnosti do druhé.

3. Technické Constraints (Tvůj checklist)
   Aby to nebylo jen o "CRUDu", zkus implementovat i tyto klíčové koncepty:

N+1 Problém: Vyřeš efektivní načítání zařízení pro seznam místností pomocí DataLoaders. Sleduj v logu (nebo v SQL profileru, pokud přidáš DB), kolik dotazů se reálně vykoná.

Input Validation: Implementuj validaci vstupů (např. název zařízení nesmí být prázdný).

Custom Scalars: Zkus přidat vlastní skalární typ, například pro datum poslední aktivity zařízení (DateTime).

Enums: Použij GraphQL Enum pro typy místností a zařízení.

Kritéria úspěchu
Až budeš hotový, měl bys být schopen v rozhraní (např. Banana Cake Pop) spustit jeden dotaz, který ti vrátí celou hierarchii domácnosti:

GraphQL
query {
home(id: 1) {
name
rooms {
name
devices {
name
type
isOnline
}
}
}
}