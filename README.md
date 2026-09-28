# Agua, Tierra y Fuego

Juego por turnos tipo piedra, papel o tijera, hecho en C# para el Trabajo Práctico Integrador de Programación Orientada a Objetos (Ingeniería en Sistemas de Información, UAP).

Un jugador humano se enfrenta a una inteligencia artificial. Cada uno recibe 5 elementos al azar (Agua, Tierra o Fuego) con 100% de energía, y en cada ronda los elementos se atacan según una tabla de daño configurable. Gana quien deja al rival sin elementos en pie.

## Video

<!-- Pegar acá el link del video subido desde github.com (arrastrando el .mp4 al editor del README). -->

## Cómo ejecutarlo

Requisito: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
git clone https://github.com/Lucavcolazo/tp1Paradigmas.git
cd tp1Paradigmas
dotnet run
```

Se juega desde la terminal: se eligen opciones escribiendo el número y apretando Enter.

## Cómo se juega

1. Elegís contra qué IA jugar (o dejás que se sortee).
2. Se reparten 5 elementos al azar para cada jugador y se muestran la tabla de daño y los dos equipos.
3. En cada ronda:
   - Si no tenés un elemento en combate (al empezar o porque el tuyo cayó), elegís uno de tus elementos vivos.
   - La IA elige el suyo mirando cuál mandaste vos y cuánta energía le queda.
   - Atacás vos primero. Si el elemento de la IA cae, la IA manda un reemplazo que contraataca en la misma ronda; si no cae, contraataca el mismo.
   - Se muestra un resumen de la ronda y, si alguien quedó fuera de combate, el estado de los dos equipos.
4. Cuando un elemento llega a 0% queda fuera de combate. La partida termina cuando un jugador se queda sin elementos vivos.

Un elemento pelea hasta caer: no se puede cambiar por otro mientras siga vivo.

## Las IAs

| IA | Cómo elige su elemento |
|---|---|
| IA Aleatoria | Al azar, sin mirar al rival ni la tabla. |
| IA Estratégica | El de mayor ventaja: daño que hace menos daño que recibe. Si empatan, el de más energía. |
| Super IA | Si algún elemento remata al rival de un golpe, usa el que lo hace con el daño justo (y, si empatan, el más gastado), guardando los mejores para después. Si ninguno lo remata, elige como la IA Estratégica. |
| Sorpresa | Toca una de las tres anteriores al azar. |

Las IAs solo eligen cuando empieza la partida o cuando cae su elemento en combate.

## Tabla de daño

Daño que le hace el atacante (fila) al defensor (columna):

| Atacante / Defensor | Agua | Tierra | Fuego |
|---|---|---|---|
| **Agua** | 15% | 20% | 50% |
| **Tierra** | 50% | 15% | 20% |
| **Fuego** | 20% | 40% | 15% |

La tabla está en `Models/EffectivenessTable.cs`. Se puede cambiar cualquier valor con `AddRule()` sin tocar la lógica del juego.

## Estructura del proyecto

```
tp1Paradigmas/
├── Program.cs              Punto de entrada: arma los objetos y arranca la partida
├── tp1Paradigmas.csproj
├── Models/                 ElementType, ElementalUnit, Pokemon, EffectivenessTable
├── Players/                Player, HumanPlayer, AIPlayer
├── AI/                     IStrategy, RandomAI, StrategicAI, SuperAI
├── Setup/                  PokemonGenerator, StrategySelector
├── Game/                   Match (las rondas)
├── UI/                     Screen (todo lo que se muestra en la consola)
└── docs/                   Diagrama de clases interactivo
```

| Clase | Responsabilidad |
|---|---|
| `Program` | Crea la pantalla, la tabla, los jugadores y la partida, y la arranca. |
| `Match` | Maneja las rondas: prepara unidades, ordena los ataques y detecta al ganador. |
| `Player` | Clase abstracta: unidades del jugador, unidad activa y reemplazo cuando cae. |
| `HumanPlayer` / `AIPlayer` | Definen cómo elige cada uno: por pantalla o con su estrategia. |
| `IStrategy` | Interfaz que cumple toda IA. |
| `RandomAI` / `StrategicAI` / `SuperAI` | Las tres formas de elegir. |
| `ElementalUnit` / `Pokemon` | Unidad de combate: tipo, energía, ataque y daño recibido. |
| `EffectivenessTable` | Matriz de daño entre tipos. |
| `PokemonGenerator` | Reparte elementos al azar. |
| `StrategySelector` | Lista de IAs disponibles para el menú. |
| `Screen` | Entrada y salida por consola. Ninguna otra clase escribe en la consola. |

## Programación orientada a objetos

En el código, cada uso está marcado con un comentario `[Herencia]`, `[Polimorfismo]`, `[Abstracción]` o `[Encapsulamiento]`.

- **Herencia:** `Pokemon` hereda de `ElementalUnit`; `HumanPlayer` y `AIPlayer` heredan de `Player`; `SuperAI` hereda de `StrategicAI` y reutiliza su lógica con `base.Choose()`.
- **Polimorfismo:** `Player.PrepareUnit()` llama a `ChooseUnit()` sin saber si el jugador es humano o IA, y `AIPlayer` llama a `Choose()` sin saber qué IA tiene. En tiempo de ejecución se ejecuta la versión de la clase real.
- **Abstracción:** `ElementalUnit` y `Player` son clases abstractas, e `IStrategy` es una interfaz: definen qué se hace y las clases concretas definen cómo.
- **Encapsulamiento:** la energía tiene setter privado y solo cambia con `TakeDamage()`; las listas internas son privadas y se exponen de solo lectura; `Screen` oculta todos sus detalles de dibujo.

Patrones de diseño usados: **Strategy** (las IAs intercambiables detrás de `IStrategy`) y **Template Method** (`PrepareUnit()` define el algoritmo y deja `ChooseUnit()` a las subclases).

Las interacciones entre tipos no se resuelven con `if` ni `switch`: el daño sale de una sola consulta a la tabla, y el comportamiento de cada IA y cada jugador, del polimorfismo.

## Diagrama de clases

[`docs/diagrama-de-clases.html`](docs/diagrama-de-clases.html) es un diagrama interactivo pensado para presentar: muestra qué le pide cada clase a otra y qué devuelve, y tiene un recorrido paso a paso de una partida en el que se puede elegir la IA. Hay que descargarlo y abrirlo con un navegador (GitHub muestra solo el código fuente).

## Decisiones de diseño

- **Agua contra Tierra hace 20%.** La consigna tiene una contradicción: en la tabla de ejemplo figura 30%, pero en el ejemplo de partida el agua le quita 20% a la tierra. Se tomó el valor del ejemplo de partida.
- **Mismo tipo contra mismo tipo hace 15%.** La consigna no lo define; se eligió un daño parejo para que esas rondas avancen.
- **El humano ataca primero en cada ronda** y el reemplazo de la IA contraataca apenas entra, siguiendo el ejemplo de partida de la consigna.
