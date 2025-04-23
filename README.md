# Introducción
El juego que se presenta en este repositorio es un *first person shooter* que consiste en el control de un personaje que tiene que atravesar una zona de bosque en medio de dos montañas, luego cruzar la base subterránea del enemigo para llegar al punto de extracción. En el camino tendrá que esquivar o eliminar a robots que disparan fuego; además, se encontrará con armas, escudos y kits de primeros auxilios que le serán útiles. También, tendrá que recoger tarjetas de acceso que le permitirán atravesar las distintas zonas para llegar a su destino.

# Controles del juego
Los controles para el movimiento del personaje están configurados de la siguiente manera:
- Movimiento: con las teclas W, S, A, D o con las flechas del teclado
- Correr: con las teclas anteriores junto con la tecla SHIFT.
- Saltar: con la barra espaciadora.
- Mirar alrededor: con el desplazamiento del mouse.
- Disparar: con el botón izquierdo del mouse.
- Apuntar: manteniendo presionado el botón derecho que mouse.
- Cambiar de arma: con la rueda central del mouse.
- Recargar el arma: con la tecla R.
- Interactuar con las puertas bloqueadas: con la tecla E.
- Mover la plataforma móvil: con las teclas H y V; movimiento horizontal y vertical respectivamente.
- Usar un kit de primeros auxilios con la tecla 1.
- Usar un escudo con la tecla 2.

Los controles anteriores se programaron usando el nuevo *input system* de Unity. Adicionalmente, el movimiento del personaje se hace con el CharacterController.
Por otro lado, el jugador tiene una barra de vida y una de escudo. La primera se vera reducida cada vez que reciba un ataque del enemigo y entre mas cerca mas daño recibirá. Sin embargo, si el jugador tiene algo en la barra de escudo, esta será la que reciba la mayor reducción.
Por último, dentro de los ítems que se pueden ir recogiendo hay 5 tipos de armas que tienen distintas cadencias de disparo, distintas capacidades de balas, provocan distintas cantidades de daño y tienen alcances distintos. También, hay una clase de escudo y de kit de primeros auxilios, así como 3 tipos de tarjetas de acceso (bronce, plata y oro) que permiten el acceso a ciertas áreas.

# Aspectos de diseño, desarrollo e implementación
### Escenas
El juego cuenta con un total de 8 escenas:
1. **InitMenu:** donde se muestran los botones que permiten iniciar o salir del juego.
2. **LevelsBase:** es la escena base donde se van cargando y descargando las escenas que contienen los niveles del juego.
3. **Level_1:** contiene la información del primer nivel del juego (la zona boscosa).
4. **Level_2:** contiene la información del segundo nivel del juego (la base subterránea del enemigo).
5. **Level_End:** contiene la información de la escena final del juego (paisaje montañoso).
6. **GameOverScene:** es la escena que se muestra una vez muere el jugador. Contiene botones para reiniciar o volver al menú inicial.
7. **EndGame:** es la escena que se muestra una vez el jugador llega al objetivo. Contiene botones para volver al menú inicial o salir del juego.
8. **All Levels:** es una escena que no se carga en el *build* y donde están todos los niveles cargados. Esta escena se uso para hacer la separación de los niveles en escenas.

### Game world
El mundo del juego consiste principalmente de 3 niveles:
1. El primer nivel es una zona boscosa en medio de dos montañas que tiene un camino para guiar al jugador. Este nivel se diseñó con la herramienta *Terrain* de Unity, con la que se cargó la topografía de una zona de 20Km x 20Km del parque Yosemite en Estados Unidos y se escalo a 1:100. Para ello se usó la herramienta de generación de mapas que se usa en el juego *Cities: Skylines* y que está disponible en la página https://heightmap.skydark.pl/
2. El segundo nivel es un área cerrada subterránea rodeada de pasillos donde el jugador tendrá que luchar con los enemigos que se encuentre y buscar las tarjetas de acceso que le permitan salir de allí. Este nivel se diseñó usando los objetos disponibles en el paquete gratuito de la Unity *asset store* llamado *Extreme Sci-Fi LITE*.
3. El tercer nivel no es mas que un paisaje a donde el jugador llega y que representa el final del juego. Este nivel se hizo con la intención de mostrar el proceso que se seguiría en caso de que este no fuese el final. El diseño del terreno se llevó a cabo de la misma manera que el nivel 1. 
### Ítems
Estos, como se menciono antes, se componen de armas, kits de primeros auxilios, escudos y tarjetas de acceso, para los cuales se programaron *scriptable objects* que almacenan toda su información. Además, se crearon otros *scriptable objects* que hacen las veces de inventarios y donde el jugador irá almacenando todo lo que recolecte por el camino. Así mismo, se creó un *scriptable object* a modo de repositorio donde se almacenan todos los ítems creados para el juego, con la intención de tener todas las referencias en un solo lugar.  De modo que, el procedimiento de creación de un ítem pasa por el siguiente procedimiento:
1. crear el *prefab* del ítem.
2. Crear el *scriptable object* de acuerdo al tipo de ítem, asignar los valores de sus características y enlazar el *prefab* e iconos correspondientes.
3. Agregar el *scriptable object* anterior a la lista correspondiente en el repositorio de ítems (**itemRepositoryScrObj.asset**)
Es importante mencionar que es necesario que cada ítem tenga un nombre único y que el *gameobject* tenga el mismo nombre, porque una vez el objeto *player* colisiona con otro objeto cuyo tag es Ítem entonces lanza un evento informando el nombre del ítem recogido, luego la clase **GameManager** escucha dicho evento y hace una consulta, por nombre, al repositorio de ítems y este último devuelve el ítem correspondiente para que el *player* incluya su referencia en su respectivo inventario.



### Personajes
##### **Player**
El objeto que controla el jugador no es mas que un cilindro que tiene su *MeshRenderer* deshabilitado y al cual se le agregaron los siguientes componentes:
- **CharacterController:** para controlar los movimientos del objeto.
- **AudioSource:** para reproducir los distintos sonidos.
- **PlayerManager:** *script* que controla todos los aspectos de daño, salud y escudo del jugador.
- **PlayerMove:** *script* que controla todos los aspectos de movimiento del jugador según los datos recibidos del input.
- **PlayerItemsManager:** *script* que controla todos los aspectos de recolección y almacenaje de ítems.
- **PlayerWeaponManager:** *script* que controla todos los aspectos de selección, cambio y recarga del arma. 
- **PlayerAccesSecurityManager:** *script* que controla todos los aspectos de nivel de seguridad que tiene el jugador.

##### **Enemy**
Los enemigos consisten de robots cuyos *prefab* se obtuvieron de la tienda de Unity y a los cuales se les adicionaron los siguientes componentes:
- **Animator:** para controlar las animaciones del personaje.
- **BoxCollider:** para que el jugador detecte el impacto de bala.
- **NavMeshAgent:** para designar como un agente y usar el módulo de IA de Unity.
- **Damage:** *script* que controla el daño que recibe el personaje y su muerte.
- **PlayerDetector:** *script* que controla el campo de visión del personaje y detecta si el jugador esta dentro de dicho campo o si está cerca.
- **WanderAgentState:** *script* que cuando está activo controla al personaje para que simule un comportamiento de vigilancia con desplazamiento aleatorios.
- **PursuitState:** *script* que cuando está activo controla al personaje para que persiga al jugador o en su defecto vaya al ultimo lugar donde lo vio.
- **ShootState:** *script* que cuando está activo controla al personaje para que ataque al jugador siempre y cuando se encuentre en un rango dado.
- **InvetigateState:** *script* que cuando está activo controla al personaje para que gire 360 grados cuando detecta que el jugador está cerca.

Los últimos cuatro componentes hacen parte de una máquina de estados que controla las distintas acciones del personaje. Para ello el estado/componente **WanderAgentState** es el que siempre empieza activo y una vez surjan las condiciones para cambiar de estado activa el correspondiente componente y se desactiva a si mismo; de igual manera se hace con los demás estados/componentes. Vale la pena mencionar que estos heredan de la clase **BaseState** que incluye los comportamientos generalizados.

### Otras clases y estructura
La programación del juego se basa en eventos, para ello se crearon una serie de buses o canales para lograr un mayor desacoplamiento y que sirven de intermediarios entre la clase que lanza un evento y las clases que lo escuchan; para ello se usaron *scriptable objects* los cuales son:
- **InputEventBusScrObj:** es el bus que canaliza los eventos lanzados por el **input system** de Unity cada vez que se usan los distintos controles del juego.
- **EnemyEventBusScrObj:** es el bus que canaliza los eventos lanzados por un personaje enemigo, que incluye un evento cuando se causa daño al jugador.
- **PlayerManagerEventBusScrObj:** es el bus que canaliza los eventos lanzados por la clase **PlayerManager**, que incluye eventos para cuando se recoge un ítem, se recarga el arma, se cambia de arma, cuando se actualizan la cantidad de vida y escudo del jugador, cuando el jugador muere o finaliza, entre otros.
- **PostMessageEventBusScrObj:** es el bus que canaliza los mensajes que se desean imprimir en pantalla.
- **WeaponEventBusScrObj:** es el bus que canaliza los eventos lanzados por la clase **Weapon**, que incluye un evento cuando se dispara el arma.

Adicionalmente, aparte de las clases usadas para el control de los personajes se crearon otras como:
- **GameManager:** es la clase que se encarga principalmente del control de la pantalla de información y de los cambios de escena cuando el jugador termina o pierde.
- **AutomaticDoor:** es la clase que controla la apertura y cierre de ciertas puertas en relación a su nivel de seguridad y el nivel de acceso que tiene el jugador.
- **MobilePlatform:** es la clase que permite que la plataforma se mueva horizontal o verticalmente, además de enviarle la información al jugador para que este se mueva en relación a ella.

Por último, a parte de las clases mencionas anteriormente también se crearon unas clases, y *scriptable objects* de apoyo, que son:
- **ButtonsActions:** contiene las funcionalidades de los distintos botones.
- **GameSettingsScrObj:** almacena las configuraciones generales del juego.
- **PlayerSettingsScrObj:** almacena las configuraciones generales del jugador.
- **EnemySettingsScrObj:** almacena las configuraciones generales de los enemigos.
- **ScreenMessagesScrObj:** almacena los distintos mensajes que se pueden mostrar por pantalla.
- **StateBar:** controla una barra de estado del jugador.
- **SceneLoader:** carga o descarga una escena según sea el caso.

### Otros aspectos
Finalmente, se usó **Cinemachine** para controlar la cámara del jugador, es decir que los movimientos para mirar alrededor se controlan desde allí. Por otro lado, la carga de las distintas escenas se hace de la siguiente manera:
- Hay una escena base que contiene los *collider* que delimitan las distintas zonas de los distintos niveles.
- Una vez se da inicio al juego, se carga la escena base y de forma aditiva se carga la escena del primer nivel.
- Cuando el jugador entra en el *collider* de una zona que delimita un nivel carga la escena de dicho nivel y cuando sale la descarga.

# Referencias
### Herramienta terreno real
https://cs.heightmap.skydark.pl/
### Texturas de *terrain*
https://assetstore.unity.com/packages/2d/textures-materials/floors/outdoor-ground-textures-12555
### Árboles
https://assetstore.unity.com/packages/3d/vegetation/trees/dream-forest-tree-105297
### Sonidos
https://www.zapsplat.com/
https://freesound.org/
### Asset nivel 2
https://assetstore.unity.com/packages/3d/environments/sci-fi/extreme-sci-fi-lite-50727
### Armas
https://assetstore.unity.com/packages/3d/props/guns/modern-weapons-pack-14233
https://assetstore.unity.com/packages/3d/props/guns/modern-guns-handgun-129821
### Enemigos
https://assetstore.unity.com/packages/3d/characters/robots/scifi-enemies-and-vehicles-15159
### Kits primeros auxilios y escudos
https://assetstore.unity.com/packages/3d/props/first-aid-kit-army-148353

