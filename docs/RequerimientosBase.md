# Documento de Requerimientos Base

## 1. Informacion general
- **Estudiante:** Albert Rafael Hernndez Osoria
- **Asignatura:** Programacion I / Programacion Orientada a Objetos en C#
- **Proyecto:** Gestor de Prestamo de Equipos Audiovisuales
- **Tipo de sistema:** Sistema academico-administrativo

## 2. Problematica
En instituciones educativas y centros audiovisuales es comun el uso compartido de camaras, proyectores, tripodes, microfonos y otros accesorios. Cuando el control se lleva en papel o de forma informal, se generan errores frecuentes: equipos perdidos, entregas tardias, poca visibilidad sobre disponibilidad y ausencia de historial de danos.

## 3. Necesidad del sistema
Se requiere una aplicacion que permita registrar usuarios, equipos, prestamos y devoluciones, controlando:
- Disponibilidad de cada equipo.
- Fecha de salida y fecha estimada de entrega.
- Estado del equipo al regresar.
- Historial de danos o incidencias.
- Diferenciacion entre tipos de equipos audiovisuales.

## 4. Objetivo general
Desarrollar un sistema orientado a objetos en C# que gestione el prestamo de equipos audiovisuales con persistencia en base de datos y estructura por capas.

## 5. Objetivos especificos
- Registrar usuarios del sistema.
- Registrar camaras, proyectores y accesorios.
- Crear prestamos y devoluciones.
- Marcar equipos con danos o en mantenimiento.
- Consultar equipos disponibles y prestamos activos.
- Aplicar principios de POO en un caso del mundo real.

## 6. Requerimientos funcionales
1. El sistema debe permitir agregar usuarios.
2. El sistema debe permitir agregar equipos audiovisuales.
3. El sistema debe validar que un equipo este disponible antes de prestarlo.
4. El sistema debe registrar la fecha del prestamo y la fecha estimada de devolucion.
5. El sistema debe registrar observaciones de entrega y devolucion.
6. El sistema debe permitir reportar danos en la devolucion.
7. El sistema debe actualizar automaticamente el estado del equipo.
8. El sistema debe mostrar el listado de equipos y prestamos.

## 7. Requerimientos no funcionales
- El proyecto debe estar dividido por capas.
- Debe existir una base de datos para almacenar la informacion.
- Debe existir una arquitectura distribuida.
- El codigo debe ser legible, reutilizable y mantenible.
- La solucion debe poder ejecutarse localmente sin configuraciones complejas.

## 8. Tecnologias propuestas
- Lenguaje: C#
- Plataforma: .NET 9
- API distribuida: ASP.NET Core Minimal API
- Base de datos: MySQL
- Acceso a datos: Entity Framework Core
- Cliente distribuido: Aplicacion de consola

## 9. Alcance
El sistema cubre el ciclo basico de registrar equipos, registrar usuarios, prestar equipos, devolverlos y documentar danos. No contempla autenticacion, reportes avanzados ni gestion multi-sucursal en esta primera version.
