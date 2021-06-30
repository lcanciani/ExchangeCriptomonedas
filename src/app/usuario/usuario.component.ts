import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {

   urlImagenBTC : string ='https://s2.coinmarketcap.com/static/img/coins/64x64/1.png';

   compraBTC: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }
  selectedBTC(){
    this.compraBTC = true;
  }
}
