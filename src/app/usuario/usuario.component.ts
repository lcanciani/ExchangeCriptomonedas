import { Component, OnInit } from '@angular/core';
import {UsuarioService} from '../usuario/usuario.service';
import { CriptomonedaModel } from './criptomoneda.model';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {

   urlImagenBTC : string ='https://s2.coinmarketcap.com/static/img/coins/64x64/1.png';

   compraBTC: boolean = false;
   listCriptomonedas : CriptomonedaModel[] ;
    public columnas : string[] = ['id','nombre'];
  constructor( private usuarioService: UsuarioService) {

    
   }

  ngOnInit(): void {

    this.usuarioService.getCriptomonedas().subscribe(response => {
      this.listCriptomonedas = response;
      console.log(response);
    })
  }
  selectedBTC(){
    //this.compraBTC = true;
  }
  openAdd(){
    
  }
}
