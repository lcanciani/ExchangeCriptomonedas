import { Component, OnDestroy, OnInit } from '@angular/core';
import { CriptomonedaModel } from '../criptomoneda.model';
import {CriptomonedasService} from '../criptomonedas.service';
import { FormGroup, FormControl, FormBuilder, Validators  } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-form-cripto',
  templateUrl: './form-cripto.component.html',
  styleUrls: ['./form-cripto.component.css']
})
export class FormCriptoComponent implements OnInit, OnDestroy {
  subscription : Subscription;
  criptomonedaModel : CriptomonedaModel
  public criptomonedasForm: FormGroup;
   _titulo: string = 'Agregar criptomoneda';
   _agregarEditarButton: string = 'Agregar';
 private id: number = 0;
 
  constructor(private criptomonedasService: CriptomonedasService,
              private formBuilder: FormBuilder,
              private snackBar: MatSnackBar) { }


  ngOnInit(): void {

    this.criptomonedasForm = this.formBuilder.group({
       
      nombre:[''], 
      precioCompra:[''],
     stockDisponible:[''], 
      simbolo:[''], 
     porcentajeGanancia :[''],
     stockTotal:[''],
      precioVenta :[''],
    imagenUrl: [''],
    fechaBaja: ['']
    });

    this.subscription = this.criptomonedasService.editarCripto().subscribe(data => {
      this.criptomonedaModel = data;
      
     
      
      this.criptomonedasForm.patchValue({
        nombre : this.criptomonedaModel.nombre,
        precioCompra: this.criptomonedaModel.precioCompra,
        stockDisponible: this.criptomonedaModel.stockDisponible,
        simbolo: this.criptomonedaModel.simbolo,
        porcentajeGanancia: this.criptomonedaModel.porcentajeGanancia,
        stockTotal: this.criptomonedaModel.stockTotal,     
        imagenUrl: this.criptomonedaModel.imagenUrl,
        fechaBaja: this.criptomonedaModel.fechaBaja
      });
      this.id = this.criptomonedaModel.idCriptomoneda;
      
      if(this.id !== undefined){

      
      this._titulo = "Editar criptomoneda";
      
      this._agregarEditarButton = "Editar";
    }
    });

  }
ngOnDestroy(){
  this.subscription.unsubscribe();
}

  addCriptomoneda(){

   
    this.criptomonedasService.addCriptomoneda(this.criptomonedasForm.value).subscribe(Response => {
      this.snackBar.open('Criptomoneda insertada con éxito','',{
        duration: 2000
      });
    });
  }
  agregarEditar(){
    if(this.id !== undefined){
      this.addCriptomoneda();
    }else{
      this.editCriptomoneda();
    }
  }

editCriptomoneda(){
  this.criptomonedasService.editarCriptomoneda(this.criptomonedasForm.value).subscribe(Response => {
    this.snackBar.open('Criptomoneda editada con éxito','',{
      duration: 2000
    });
  });
}

}
