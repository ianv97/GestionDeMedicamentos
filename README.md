# TP Final 2019 - Grupo 1 - Administración de Medicamentos

## Colaboradores
1. Fierro Cáceres, Victoria
2. Gallardo, Madelein
3. Maidana, Lucas
4. Stehr, Sofía
5. Vaernet, Ian

## Links de la aplicación
1. [Inicio](http://medicamentos.us-east-1.elasticbeanstalk.com); 
2. [Drogas](http://medicamentos.us-east-1.elasticbeanstalk.com/Drogas);
3. [Medicamentos](http://medicamentos.us-east-1.elasticbeanstalk.com/Medicamentos);
4. [Reposiciones](http://medicamentos.us-east-1.elasticbeanstalk.com/Reposiciones);
5. [Partidas](http://medicamentos.us-east-1.elasticbeanstalk.com/Partidas);
6. [Stock](http://medicamentos.us-east-1.elasticbeanstalk.com/Stock).

## Links de la API
1. [API Drogas](http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas);
2. [API Medicamentos](http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos);
3. [API Reposiciones](http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones);
4. [API Partidas](http://medicamentos.us-east-1.elasticbeanstalk.com/api/partidas);
5. [API Stock](http://medicamentos.us-east-1.elasticbeanstalk.com/api/stock).


#### Funcionalidad de la Aplicación
La aplicación desarrollada, permite realizar altas, bajas, modificaciones y consultas (CRUD), de los medicamentos, de las reposiciones y de las partidas que se encuentran en stock en el hospital.
#### Arquitectura empleada
La arquitectura de software empleada es Modelo-Vista-Controlador, la cual separa los datos, la interfaz de usuario, y la lógica de control en tres componentes distintos: Modelo que contiene una representación de los datos que maneja el sistema, su lógica de negocio, y sus mecanismos de persistencia. Vista, o interfaz de usuario, que compone la información que se envía al cliente y los mecanismos interacción con éste. Controlador, que actúa como intermediario entre el Modelo y la Vista, gestionando el flujo de información entre ellos y las transformaciones para adaptar los datos a las necesidades de cada uno.
#### Acceso a datos
El servicio realiza el acceso a datos a través de la una ORM.

#### Lenguaje de programación Backend: C#
C# es un lenguaje de programación orientado a objetos desarrollado y estandarizado por Microsoft como parte de su plataforma .NET. Su sintaxis es similiar al del lenguaje de programacion Java, aunque incluye aspectos derivados de otros lenguajes. 

#### Framework Backend: .NET Core
.NET Core es un marco de software gratuito y de código abierto multiplataforma, compatible con Windows, Linux y macOS. Es un sucesor de código abierto y multiplataforma de .NET Framework. El proyecto está principalmente desarrollado por Microsoft y publicado bajo la licencia MIT.

#### Lenguaje de programación Frontend: JavaScript
JavaScript es un lenguaje de programación interpretado, dialecto del estándar ECMAScript. Se define como orientado a objetos, basado en prototipos, imperativo, débilmente tipado y dinámico. Se utiliza principalmente en su forma del lado del cliente, implementado como parte de un navegador web permitiendo mejoras en la interfaz de usuario y páginas web dinámicas aunque también existe una forma de JavaScript del lado del servidor.

#### Framework Frontend: ReactJS
ReactJS es una biblioteca JavaScript de código abierto diseñada para crear interfaces de usuario con el objetivo de facilitar el desarrollo de aplicaciones en una sola página (SPA). Es mantenido por Facebook y la comunidad de software libre. React intenta ayudar a los desarrolladores a construir aplicaciones que usan datos que cambian todo el tiempo. Su objetivo es ser sencillo, declarativo y fácil de combinar.

#### Base de datos: PostgreSQL
PostgreSQL es un sistema de gestión de bases de datos relacional y de código abierto, publicado bajo la licencia PostgreSQL, similar a la BSD o la MIT. El desarrollo de PostgreSQL no es manejado por una empresa o persona, sino que es dirigido por una comunidad de desarrolladores que trabajan de forma desinteresada denominada PGDG (PostgreSQL Global Development Group).

#### Entorno de desarrollo: Visual Studio 2019
Visual Studio es un entorno de desarrollo integrado para Windows, Linux y macOS. Es compatible con múltiples lenguajes de programación, tales como C++, C#, Visual Basic .NET, F#, Java, Python, Ruby y PHP, al igual que entornos de desarrollo web, como ASP.NET MVC, Django, etc. Está orientado a permitir a los desarrolladores crear sitios y aplicaciones web, así como servicios web en cualquier entorno compatible con la plataforma .NET
