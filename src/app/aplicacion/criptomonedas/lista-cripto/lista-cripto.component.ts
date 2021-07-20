import { Component, OnInit, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CriptomonedaModel } from '../criptomoneda.model';
import { CriptomonedasService } from '../criptomonedas.service';


@Component({
  selector: 'app-lista-cripto',
  templateUrl: './lista-cripto.component.html',
  styleUrls: ['./lista-cripto.component.css']
})
export class ListaCriptoComponent implements OnInit {

  @Input('listCriptomonedas') listCriptomonedas : CriptomonedaModel[];
  public columnas : string[] = ['id','nombre','accion'];
  criptomonedaModel : CriptomonedaModel;
  
  constructor(private criptomonedasService: CriptomonedasService,
              private _snackBar: MatSnackBar) { }

  ngOnInit(): void {

    this.criptomonedasService.getCriptomonedas().subscribe(response => {
      this.listCriptomonedas = response.data;
      
    })
  }
cargarCripto(element: CriptomonedaModel){
  
  this.criptomonedasService.cargarCripto(element);
//console.log("este es el hdp: -> ")
  //console.log(element.idCriptomoneda);
}
  
eliminarCripto(element: number){
this.criptomonedasService.eliminarCriptomoneda(element).subscribe(respuesta =>{
  if(respuesta.exito === 0 && respuesta.data !== undefined){
    this._snackBar.open('La criptomoneda ya esta dada de baja','',{
      duration: 1000,
      verticalPosition: 'top'
    })
  }
  this.listCriptomonedas = respuesta.data;
});
}
}
