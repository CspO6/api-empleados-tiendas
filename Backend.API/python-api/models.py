from sqlalchemy import Column, Integer, String, Boolean, DateTime, ForeignKey
from sqlalchemy.orm import relationship
from database import Base

from sqlalchemy import Column, Integer, String, Boolean
from database import Base

class Tienda(Base):
    __tablename__ = "Tiendas"

    Id = Column("Id", Integer, primary_key=True, index=True)
    Nombre = Column("Nombre", String)
    Direccion = Column("Direccion", String)
    EstaActiva = Column("EstaActiva", Boolean)


    empleados = relationship("Empleado", back_populates="tienda")



class Empleado(Base):
    __tablename__ = "Empleados"


    Id = Column("Id", Integer, primary_key=True, index=True)
    Nombre = Column("Nombre", String)
    Apellido = Column("Apellido", String)
    Correo = Column("Correo", String)
    Cargo = Column("Cargo", String)
    FechaIngreso = Column("FechaIngreso", DateTime)
    EstaActivo = Column("EstaActivo", Boolean)

    TiendaId = Column("TiendaId", Integer, ForeignKey("Tiendas.Id"))
    tienda = relationship("Tienda", back_populates="empleados")