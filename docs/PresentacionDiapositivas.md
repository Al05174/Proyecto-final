# Presentacion en Diapositivas

## Diapositiva 1. Portada
- Proyecto Final
- Gestor de Prestamo de Equipos Audiovisuales
- Estudiante: Albert Rafael Hernndez Osoria
- Asignatura: Programacion I

## Diapositiva 2. Problema
- En muchas instituciones el prestamo de equipos se controla manualmente.
- Esto genera retrasos, perdida de informacion y poca trazabilidad.
- No siempre se sabe que equipo esta disponible ni quien lo tiene.

## Diapositiva 3. Necesidad
- Se necesita un sistema que controle:
- Usuarios
- Equipos audiovisuales
- Prestamos y devoluciones
- Danos o incidencias

## Diapositiva 4. Idea del proyecto
- Desarrollar un sistema de gestion de prestamo de equipos audiovisuales.
- El sistema administra camaras, proyectores y accesorios.
- Permite consultar disponibilidad y registrar movimientos.

## Diapositiva 5. Objetivo general
- Crear una solucion en C# basada en Programacion Orientada a Objetos.
- Debe incluir base de datos, separacion de capas y arquitectura distribuida.

## Diapositiva 6. Tecnologias utilizadas
- C#
- .NET
- ASP.NET Core
- Entity Framework Core
- MySQL
- API REST
- Cliente de consola

## Diapositiva 7. Arquitectura
- Domain
- Application
- Infrastructure
- Api
- ConsoleClient

## Diapositiva 8. Modelo orientado a objetos
- Clase abstracta: Equipo
- Clases derivadas: Camara, Proyector, Accesorio
- Clases normales: Usuario, Prestamo
- Uso de herencia, encapsulamiento y abstraccion

## Diapositiva 9. Constructores y sobrecargas
- Constructores en Usuario y Prestamo
- Sobrecarga en RegistrarDanio
- Sobrecarga en RegistrarDevolucion

## Diapositiva 10. Base de datos
- Conexion a MySQL
- Persistencia con Entity Framework Core
- Almacenamiento de usuarios, equipos y prestamos

## Diapositiva 11. Arquitectura distribuida
- La API expone el sistema
- El cliente de consola consume la API
- La base de datos se maneja desde la capa de infraestructura

## Diapositiva 12. Funcionalidades principales
- Registrar usuarios
- Registrar equipos
- Crear prestamos
- Registrar devoluciones
- Reportar danos
- Consultar disponibilidad

## Diapositiva 13. Beneficios
- Mayor control del inventario
- Mejor seguimiento de prestamos
- Menos errores manuales
- Historial de incidencias y danos

## Diapositiva 14. Repositorio publico
- Codigo fuente del proyecto:
- https://github.com/Al05174/Proyecto-final

## Diapositiva 15. Cierre
- El proyecto cumple con los requisitos de la asignacion.
- Usa un caso del mundo real.
- Integra POO, base de datos, capas y arquitectura distribuida.
