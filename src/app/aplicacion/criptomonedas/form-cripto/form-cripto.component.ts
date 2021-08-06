import { Component, OnDestroy, OnInit, Output, EventEmitter } from '@angular/core';
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
  criptomonedaModel : CriptomonedaModel = new CriptomonedaModel();
  public criptomonedasForm: FormGroup;
   _titulo: string = 'Agregar criptomoneda';
   _agregarEditarButton: string = 'Agregar';
 private id: number = 0;
 baja: boolean = false;
 @Output() listaCriptoActualizada = new EventEmitter<CriptomonedaModel[]>();
  constructor(private criptomonedasService: CriptomonedasService,
              private formBuilder: FormBuilder,
              private snackBar: MatSnackBar) { }


  ngOnInit(): void {

    this.criptomonedasForm = this.formBuilder.group({
       idCriptomoneda:[{value:'', disabled: true}],
      nombre:['',Validators.required], 
      precioCompra:['',Validators.required],
     stockDisponible:['',Validators.required], 
      simbolo:['',Validators.required], 
     porcentajeGanancia :['',Validators.required],
     stockTotal:[''],
      
    imagenUrl: ['',Validators.required],
    fechaBaja: [{value:'', disabled: true} ]
    });

    this.subscription = this.criptomonedasService.editarCripto().subscribe(data => {
      console.log('llegue al OnInit del form: '+ data)
      
      this.criptomonedaModel = data;
      
     
      
      this.criptomonedasForm.patchValue({
        idCriptomoneda:this.criptomonedaModel.idCriptomoneda,
        nombre : this.criptomonedaModel.nombre,
        precioCompra: this.criptomonedaModel.precioCompra,
        stockDisponible: this.criptomonedaModel.stockDisponible,
        simbolo: this.criptomonedaModel.simbolo,
        porcentajeGanancia: this.criptomonedaModel.porcentajeGanancia,
        stockTotal: this.criptomonedaModel.stockTotal,     
        imagenUrl: this.criptomonedaModel.imagenUrl,
        fechaBaja: this.criptomonedaModel.fechaBaja
      });
      
      console.log('idCripto: '+this.criptomonedaModel.idCriptomoneda )
      this.id = this.criptomonedaModel.idCriptomoneda;
      
      if(this.id !== undefined){

      
      this._titulo = "Editar criptomoneda";
      
      this._agregarEditarButton = "Editar";
    }
    else{
      this._titulo = "Agregar criptomoneda";
      
      this._agregarEditarButton = "Agregar";
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
    console.log(this.id)
    if(this.id === undefined){
      this.addCriptomoneda();
    }else{
      this.editCriptomoneda();
    }
  }

editCriptomoneda(){

  
  this.criptomonedaModel.idCriptomoneda = this.criptomonedasForm.get('idCriptomoneda').value;
  this.criptomonedaModel.nombre = this.criptomonedasForm.get('nombre').value;
  this.criptomonedaModel.precioCompra = this.criptomonedasForm.get('precioCompra').value;
  this.criptomonedaModel.stockDisponible = this.criptomonedasForm.get('stockDisponible').value;
  this.criptomonedaModel.simbolo = this.criptomonedasForm.get('simbolo').value;
  this.criptomonedaModel.porcentajeGanancia = this.criptomonedasForm.get('porcentajeGanancia').value;
  this.criptomonedaModel.stockTotal = this.criptomonedasForm.get('stockTotal').value;
  this.criptomonedaModel.imagenUrl = this.criptomonedasForm.get('imagenUrl').value;
  this.criptomonedaModel.fechaBaja = this.criptomonedasForm.get('fechaBaja').value;
  const idCripto = this.criptomonedasForm.get('idCriptomoneda').value;
  console.log('idCriptooo: '+this.criptomonedaModel.idCriptomoneda + 'pero mira:' + this.criptomonedasForm.get('idCriptomoneda').value) 
  this.criptomonedasService.editarCriptomoneda(this.criptomonedaModel).subscribe(Response => {
    this.listaCriptoActualizada.emit(Response.data);
    this.snackBar.open('Criptomoneda editada con éxito','',{
      duration: 2000
    });
  });
}

darDeAlta(){
  this.criptomonedasForm.patchValue({
    fechaBaja: null
      });
}

}
