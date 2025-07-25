from fastapi import FastAPI, Depends, HTTPException, Response 
from sqlalchemy.orm import Session
from database import SessionLocal, engine
import models
import io
from reportlab.lib.pagesizes import letter
from reportlab.platypus import SimpleDocTemplate, Table, TableStyle, Paragraph, Spacer
from reportlab.lib.styles import getSampleStyleSheet
from reportlab.lib import colors
from fastapi.middleware.cors import CORSMiddleware


app = FastAPI()

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"], 
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

def get_db():
    db = SessionLocal() 
    try:
        yield db
    finally:
        db.close()

@app.get("/")
def root():
    return {"mensaje": "API operativa correctamente"}

@app.get("/empleados")
def listar_empleados(db: Session = Depends(get_db)):
    empleados = db.query(models.Empleado).all()
    return [emp.__dict__ for emp in empleados] 

@app.get("/tiendas")
def listar_tiendas(db: Session = Depends(get_db)):
    tiendas = db.query(models.Tienda).all()
    return [tienda.__dict__ for tienda in tiendas]


@app.get("/reporte-empleados-pdf")
def generar_reporte_empleados_pdf(db: Session = Depends(get_db)):

    empleados = db.query(models.Empleado).all()


    buffer = io.BytesIO()
    doc = SimpleDocTemplate(buffer, pagesize=letter)
    elements = []
    styles = getSampleStyleSheet()

    title = Paragraph("Reporte de Todos los Empleados", styles['Title'])
    elements.append(title)
    elements.append(Spacer(1, 12))


    data = [["ID", "Nombre", "Apellido", "Correo", "Cargo", "Tienda ID", "Activo"]]

    for emp in empleados:
        data.append([
            str(emp.Id),
            emp.Nombre or '',
            emp.Apellido or '',
            emp.Correo or '',
            emp.Cargo or '',
            str(emp.TiendaId) if emp.TiendaId else '',
            "Sí" if emp.EstaActivo else "No"
        ])


    table = Table(data)
    table.setStyle(TableStyle([
        ('BACKGROUND', (0, 0), (-1, 0), colors.grey),
        ('TEXTCOLOR', (0, 0), (-1, 0), colors.whitesmoke),
        ('ALIGN', (0, 0), (-1, -1), 'CENTER'),
        ('FONTNAME', (0, 0), (-1, 0), 'Helvetica-Bold'),
        ('FONTSIZE', (0, 0), (-1, 0), 8),
        ('BOTTOMPADDING', (0, 0), (-1, 0), 10),
        ('BACKGROUND', (0, 1), (-1, -1), colors.beige),
        ('GRID', (0, 0), (-1, -1), 1, colors.black)
    ]))

    elements.append(table)
    

    doc.build(elements)
    

    buffer.seek(0)
    headers = {
        'Content-Disposition': 'attachment; filename="reporte_todos_empleados.pdf"'
    }
    return Response(content=buffer.getvalue(), headers=headers, media_type='application/pdf')
@app.get("/reporte-tiendas-pdf")
def generar_reporte_tiendas_pdf(db: Session = Depends(get_db)):

    tiendas = db.query(models.Tienda).all()

    buffer = io.BytesIO()
    doc = SimpleDocTemplate(buffer, pagesize=letter)
    elements = []
    styles = getSampleStyleSheet()

    title = Paragraph("Reporte de Todas las Tiendas", styles['Title'])
    elements.append(title)
    elements.append(Spacer(1, 12))

    data = [["ID", "Nombre", "Dirección", "Activa"]]

    for tienda in tiendas:
        data.append([
            str(tienda.Id),
            tienda.Nombre or '',
            tienda.Direccion or '',
            "Sí" if tienda.EstaActiva else "No"
        ])

    table = Table(data)
    table.setStyle(TableStyle([
        ('BACKGROUND', (0, 0), (-1, 0), colors.grey),
        ('TEXTCOLOR', (0, 0), (-1, 0), colors.whitesmoke),
        ('ALIGN', (0, 0), (-1, -1), 'CENTER'),
        ('FONTNAME', (0, 0), (-1, 0), 'Helvetica-Bold'),
        ('FONTSIZE', (0, 0), (-1, 0), 8),
        ('BOTTOMPADDING', (0, 0), (-1, 0), 10),
        ('BACKGROUND', (0, 1), (-1, -1), colors.beige),
        ('GRID', (0, 0), (-1, -1), 1, colors.black)
    ]))

    elements.append(table)
    
    doc.build(elements)
    
    buffer.seek(0)
    headers = {
        'Content-Disposition': 'attachment; filename="reporte_todas_tiendas.pdf"'
    }
    return Response(content=buffer.getvalue(), headers=headers, media_type='application/pdf')
