# Conexión a PostgreSQL con pgAdmin

## Datos de Conexión

Para conectarte a la base de datos PostgreSQL desde pgAdmin, usa los siguientes datos:

### Información de Conexión

| Campo | Valor |
|-------|-------|
| **Host/Server** | `localhost` |
| **Port** | `5432` |
| **Database** | `MovieSearchDB` |
| **Username** | `postgres` |
| **Password** | `postgres` |

### Pasos para Conectar en pgAdmin

1. **Abre pgAdmin** en tu computadora

2. **Crea una nueva conexión:**
   - Click derecho en "Servers" → "Register" → "Server..."

3. **En la pestaña "General":**
   - **Name**: `MovieSearch DB` (o cualquier nombre que prefieras)

4. **En la pestaña "Connection":**
   - **Host name/address**: `localhost`
   - **Port**: `5432`
   - **Maintenance database**: `MovieSearchDB`
   - **Username**: `postgres`
   - **Password**: `postgres`
   - ✅ Marca la casilla "Save password" si quieres guardar la contraseña

5. **Click en "Save"**

### Verificación

Una vez conectado, deberías ver:
- **Database**: `MovieSearchDB`
- **Schemas**: `public`
- **Tables**: 
  - `Actors`
  - `Movies`
  - `MovieActors`

### Datos de Prueba

La base de datos contiene datos iniciales (seed data):
- **8 películas** (Forrest Gump, The Revenant, etc.)
- **6 actores** (Tom Hanks, Leonardo DiCaprio, etc.)
- **Relaciones** entre películas y actores

### Notas Importantes

⚠️ **Asegúrate de que el contenedor de PostgreSQL esté corriendo:**
```bash
docker ps
```

Si no está corriendo:
```bash
docker-compose up -d postgres
```

### Troubleshooting

**Error: "Connection refused"**
- Verifica que el contenedor esté corriendo: `docker ps`
- Verifica que el puerto 5432 esté disponible
- Revisa los logs: `docker logs moviesearch-postgres`

**Error: "Authentication failed"**
- Verifica que el usuario y contraseña sean correctos: `postgres` / `postgres`
- Si cambiaste la contraseña, actualiza el `docker-compose.yml`

**Error: "Database does not exist"**
- La base de datos se crea automáticamente al iniciar el contenedor
- Si no existe, reinicia el contenedor: `docker-compose restart postgres`

### Consultas Útiles

Una vez conectado, puedes ejecutar estas consultas:

```sql
-- Ver todas las películas
SELECT * FROM "Movies";

-- Ver todos los actores
SELECT * FROM "Actors";

-- Ver películas con sus actores
SELECT 
    m."Title",
    m."Genre",
    a."Name" as ActorName
FROM "Movies" m
JOIN "MovieActors" ma ON m."Id" = ma."MovieId"
JOIN "Actors" a ON ma."ActorId" = a."Id"
ORDER BY m."Title";
```
