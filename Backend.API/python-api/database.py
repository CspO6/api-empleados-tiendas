from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, declarative_base

# Reemplaza con tus datos reales
DB_SERVER = 'localhost'
DB_NAME = 'EmpleadosTiendasDB'
DB_DRIVER = 'ODBC Driver 17 for SQL Server'
DB_TRUSTED = 'yes'

DATABASE_URL = (
    "mssql+pyodbc://localhost/EmpleadosTiendasDB"
    "?driver=ODBC+Driver+17+for+SQL+Server"
    "&trusted_connection=yes"
)

engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

Base = declarative_base()
