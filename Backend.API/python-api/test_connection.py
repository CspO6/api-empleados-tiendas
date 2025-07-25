from sqlalchemy import create_engine, text

DATABASE_URL = (
    "mssql+pyodbc://localhost/EmpleadosTiendasDB"
    "?driver=ODBC+Driver+17+for+SQL+Server"
    "&trusted_connection=yes"
)

try:
    engine = create_engine(DATABASE_URL)
    with engine.connect() as conn:
        result = conn.execute(text("SELECT 1"))
        print("✅ Conexión exitosa:", result.scalar())
except Exception as e:
    print("❌ Error en la conexión:", e)
