import { Component, OnInit } from '@angular/core';

import  jsPDF  from "jspdf";
import 'jspdf-autotable'

import { ReportesModel } from './reportes.model';
import html2canvas from 'html2canvas'; 
import {ReportesService} from '../reportes/reportes.service';
import {ReporteComprasModel} from '../reportes/reporteComprasModel.model'

import { FormGroup, FormControl, FormBuilder, Validators  } from '@angular/forms';


export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}
 
  
const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

/**
 * @title Styling columns using their auto-generated column names
 */
@Component({
  selector: 'app-reportes',
  styleUrls: ['reportes.component.css'],
  templateUrl: 'reportes.component.html',
})
export class ReportesComponent {
  displayedColumns: string[] = ['demo-position', 'demo-name', 'demo-weight', 'demo-symbol'];
  columns= [{ header: 'ID', dataKey: 'idUsuario' },
            {header: 'Apellido',dataKey:'apellido'},
            {header: 'Nombre',dataKey:'nombre'},
          {header: 'Monto Total', dataKey:'montoTotal'}] 
  //dataSource = ELEMENT_DATA;
  estadoPrecioCompra: number;
  listCompras: ReporteComprasModel[]=[];
  dataSource = this.listCompras;
  public reportesForm: FormGroup;

  constructor(private _reporteService: ReportesService, private _formBuilder: FormBuilder) {
    //this.downloadPDF();
  }
  ngOnInit(): void {
    this.reportesForm = this._formBuilder.group({
      fechaInicio:[{value:'', disabled: true} ],
      fechaFin:[{value:'', disabled: true} ]
    })
  }
  public descargarPDF(): void {
    const doc =  new jsPDF();
    const data = document.getElementById('htmlData');

    html2canvas(data).then(canvas => {
      const img = canvas.toDataURL('image/PNG');
      
// Añadir imagen Canvas a PDF
const bufferX = 15;
const bufferY = 30;
const imgProps = (doc as any).getImageProperties(img);
const pdfWidth = doc.internal.pageSize.getWidth() -2 * bufferX;
const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
doc.addImage(img, 'PNG', bufferX, bufferY, pdfWidth, pdfHeight, undefined, 'FAST');
doc.setFontSize(30);
doc.text('REPORTE DE COMPRAS',15,15);
return doc;
    }).then(docResult => {
      
    docResult.save(`${new Date().toISOString()}_ReporteCompras.pdf`);
    
    }); 
  }

  descargarPDFAutoTable(){
    
    const doc =  new jsPDF();
    doc.text('REPORTE DE COMPRAS TOTALES POR USUARIO',15,15);
    doc.autoTable({html:'#tablaReporteCompras', startY: 30,

    didDrawPage: function (data) {
    
     
  
      // Footer
      var str = "Page " + doc.internal.getNumberOfPages();
  
      doc.setFontSize(10);
  
      // jsPDF 1.4+ uses getWidth, <1.4 uses .width
      var pageSize = doc.internal.pageSize;
      var pageHeight = pageSize.height
        ? pageSize.height
        : pageSize.getHeight();
      doc.text(str, data.settings.margin.left, pageHeight - 10);
    }

    })
    doc.save(`${new Date().toISOString()}_ReporteCompras.pdf`);
  }

  getDatosReporteCompras(tipoTemporalidad: number){
    let datosReporte = new ReportesModel();
    datosReporte.tipo = tipoTemporalidad;
    this._reporteService.getReporteCompras(datosReporte).subscribe( resp =>{
      
      this.dataSource = resp.data;
    });
     
  }
  getDatosReporteComprasPorFechas(){
    
    if(this.reportesForm.get('fechaInicio').value !== '' && this.reportesForm.get('fechaFin').value !== ''){
      
      let fechaInicio = this.reportesForm.get('fechaInicio').value;
      let fechaFin = this.reportesForm.get('fechaFin').value;
         let datosReporte = new ReportesModel();
        datosReporte.fechaInicio = fechaInicio;
         datosReporte.fechaFin = fechaFin;
         datosReporte.tipo = 4;
         this._reporteService.getReporteCompras(datosReporte).subscribe( resp =>{
        
          this.dataSource = resp.data;
        });
      
    }
    else{
      
      let fechaInicio = new Date();
      let fechaFin = new Date();
      
         let datosReporte = new ReportesModel();
        datosReporte.fechaInicio = fechaInicio;
         datosReporte.fechaFin = fechaFin;
         datosReporte.tipo = 4;
      this._reporteService.getReporteCompras(datosReporte).subscribe( resp =>{
        
        this.dataSource = resp.data;
      });

    }
    
  }

// Default export is a4 paper, portrait, using millimeters for units



// Landscape export, 2×4 inches
/*

doc.text("Hello world!", 10, 10);
doc.save("a4.pdf");
If you want to change the paper size, orientation, or units, you can do:


const doc = new jsPDF({
  orientation: "landscape",
  unit: "in",
  format: [4, 2]
});
*/



}
/*, {
      //startY: doc.autoTable() + 70,
    
      margin: { horizontal: 10 },
      styles: { overflow: "linebreak" },
      bodyStyles: { valign: "top" },
      //columnStyles: { email: { columnWidth: "wrap" } },
      theme: "striped",
      showHead: "everyPage",
      didDrawPage: function (data) {
    
        // Header
        doc.setFontSize(20);
        doc.setTextColor(40);
        doc.text("Report", data.settings.margin.left, 22);
    
        // Footer
        var str = "Page " + doc.internal.getNumberOfPages();
    
        doc.setFontSize(10);
    
        // jsPDF 1.4+ uses getWidth, <1.4 uses .width
        var pageSize = doc.internal.pageSize;
        var pageHeight = pageSize.height
          ? pageSize.height
          : pageSize.getHeight();
        doc.text(str, data.settings.margin.left, pageHeight - 10);
      }
    }); */