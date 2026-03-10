classDiagram
    class Game {
        +int width
        +int height
        +int level
        +List~Player~ players
        +List~Apple~ apples
        +StartLevel()
        +Update()
        +Render()
    }

    class Player {
        +string id
        +string name
        +int score
        +Snake snake
        +Respawn()
    }

    class Snake {
        +List~Segment~ body
        +Direction currentDirection
        +Move()
        +Grow()
        +Shrink()
        +CheckCollision()
    }

    class Segment {
        +int x
        +int y
        +SegmentType type  "HEAD или BODY"
    }

    class Apple {
        <<Abstract>>
        +int x
        +int y
        +AppleType type
        +ApplyEffect(Snake)*
    }

    class NormalApple {
        +ApplyEffect(Snake)
    }

    class ExpandApple {
        +ApplyEffect(Snake)
    }

    class GoldenApple {
        +ApplyEffect(Snake)
    }

    class ConstrictingApple {
        +ApplyEffect(Snake)
    }

    Game "1" *-- "many" Player : содержит
    Game "1" *-- "many" Apple : содержит
    Player "1" *-- "1" Snake : управляет
    Snake "1" *-- "many" Segment : состоит из
    Apple <|-- NormalApple : наследуется
    Apple <|-- SpeedApple : наследуется
    Apple <|-- GoldenApple : наследуется
    Apple <|-- SlowApple : наследуется