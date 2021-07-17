import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-deslogear',
  templateUrl: './deslogear.component.html',
  styleUrls: ['./deslogear.component.css']
})
export class DeslogearComponent implements OnInit {

  constructor(private _loginService: LoginService,
              private _route: Router) { }

  ngOnInit(): void {
  }
  desloguear(){
    this._loginService.logout();
    this._route.navigate(['login']);
  }
}
