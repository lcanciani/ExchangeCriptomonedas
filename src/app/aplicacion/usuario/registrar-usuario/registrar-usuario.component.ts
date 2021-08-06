import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { UsuarioService } from '../usuario.service';

import { DepositoModel } from '../../deposito/deposito.model';
import { BancoModel } from '../../bancos/banco.model';
import { BancosService } from '../../bancos/bancos.service';
import { BancoCbu } from './bancoCbu.model';

import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { RegistrarUsuarioModel } from './registrarUsuario.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';


@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.css']
})
export class RegistrarUsuarioComponent implements OnInit {
  listBancos:BancoModel[];
  depositoModel: DepositoModel = new DepositoModel();
  registrarUsuarioForm: FormGroup;
  registrarCbuBancoFrom:FormGroup;
  private _registrarUsuarioModel: RegistrarUsuarioModel;
  public requiredCbu: boolean = true;
  bancosCbus: BancoCbu[] = [];
  estado: boolean = true;
  estadoRegistrar:boolean= true;
  bancoCbu: BancoCbu ;

  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes = [ENTER, COMMA] as const;
 
  
  constructor(private _formBuilder: FormBuilder,
              private _userService : UsuarioService,
              private _bancoService: BancosService,
              private _snackBar: MatSnackBar,
              private _route: Router) { }

  ngOnInit(): void {
    this.registrarCbuBancoFrom = this._formBuilder.group({
      banco: ['',Validators.required],
    cbu: ['',[Validators.min(99999999),Validators.max(10000000000)]]
    })

    this.registrarUsuarioForm = this._formBuilder.group({
      nombre: [''],
      apellido: [''],
      direccion: [''],
    email: [''],
    password: [''],
    dni:[''],
    
    })
    this._bancoService.getBancos().subscribe(resp =>{
      this.listBancos = resp.data;
      
    })

  }

agregar( cbu: string){
  this.bancoCbu =  new BancoCbu();

  this.bancoCbu.banco = this.registrarCbuBancoFrom.get('banco').value.razonSocial;
  this.bancoCbu.idBanco = this.registrarCbuBancoFrom.get('banco').value.idBanco;
  this.bancoCbu.cbu = cbu;

  this.bancosCbus.push(this.bancoCbu);
  if(this.bancosCbus.length > 0){

  
  this.estadoRegistrar = false;
  this.registrarCbuBancoFrom.get('cbu').reset();
  if(this.registrarCbuBancoFrom.get('cbu').value < 1)
  this.estado = true;
  else
  this.estado = false;
}
  else{
    this.estadoRegistrar = true;
  }
  //this.registrarCbuBancoFrom.get('cbu').clearValidators();
  //this.registrarCbuBancoFrom.get('cbu').reset();
  //this.registrarCbuBancoFrom.get('cbu').setValidators([Validators.min(99999999),Validators.max(10000000000)]);
  //this.registrarCbuBancoFrom.updateValueAndValidity();
  //this.requiredCbu = true;
}
registrarUsuario(){


this._registrarUsuarioModel = new RegistrarUsuarioModel();
this._registrarUsuarioModel.apellido = this.registrarUsuarioForm.get('apellido').value;
this._registrarUsuarioModel.direccion = this.registrarUsuarioForm.get('direccion').value;
this._registrarUsuarioModel.email = this.registrarUsuarioForm.get('email').value;
this._registrarUsuarioModel.password = this.registrarUsuarioForm.get('password').value;
//this._registrarUsuarioModel.dni = this.registrarUsuarioForm.get('dni').value;
console.log(this.registrarUsuarioForm.get('dni').value.toString())
this._registrarUsuarioModel.dni = this.registrarUsuarioForm.get('dni').value.toString();
  this._registrarUsuarioModel.nombre = this.registrarUsuarioForm.get('nombre').value;
  this._registrarUsuarioModel.bancoCbu = this.bancosCbus;

  this._userService.registrarUsuario(this._registrarUsuarioModel).subscribe(resp =>{
    if(resp.exito ===1){
      this._snackBar.open('Usuario registrado con éxito!','',{
        duration: 2000,
        verticalPosition: 'top'});
      this._route.navigate(['/comp']);
    }
    else{
      this._snackBar.open('Se produjo un error al intentar registrar el usuario. Intentelo nuevamente','',{
        duration: 3500,
        verticalPosition: 'top'
      })
    }
  });
}  

  remove(bancoCbu: BancoCbu): void {
    
    const index = this.bancosCbus.indexOf(bancoCbu);

    if (index >= 0) {
      this.bancosCbus.splice(index, 1);
      if(this.bancosCbus.length < 1){
       this.estadoRegistrar = true;
      }
    }
  
}


onSearchChange(value: number){
  if(value < 1)
  this.estado = true;
  else
  this.estado = false;
}

}

