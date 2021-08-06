import { Component, ElementRef, OnInit,ViewChild } from '@angular/core';
import { Chart, BarElement, BarController, CategoryScale, Decimation, Filler, Legend, Title, Tooltip, LinearScale } from 'chart.js';
import {EstadisticasService} from './estadisticas.service';
import {CriptoConsAgrup} from './criptoConsAgrup.model';

@Component({
  selector: 'app-estadisticas',
  templateUrl: './estadisticas.component.html',
  styleUrls: ['./estadisticas.component.css']
})
export class EstadisticasComponent implements OnInit {

listCriptoConsAgrup: CriptoConsAgrup[];
labels: string[] =[];
data: number [] = [];
  constructor(private _estadisticasService: EstadisticasService) { 

    Chart.register(BarElement, BarController, CategoryScale, Decimation, Filler, Legend, Title, Tooltip, LinearScale);
  }

  ngOnInit(): void {
    this._estadisticasService.getCriptoConsAgrup().subscribe(resp => {
      this.listCriptoConsAgrup = resp.data;
      this.listCriptoConsAgrup.forEach(element => {
        this.labels.push(element.nombreCripto);
        this.data.push(element.cantidadMovimientos);
        
        
        
      });
      var myChart = new Chart("myChart", {
        type: 'bar',
        data: {
            labels: this.labels,
            datasets: [{
                label: '# of Votes',
                data: this.data,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            responsive: false,
            maintainAspectRatio:false
        }
    });
    })
    
   
  }
  crearGrafico(){
    
    var ctx = 'myChart';
    this.listCriptoConsAgrup.forEach(element => {
      this.labels.push(element.nombreCripto);
      this.data.push(element.cantidadMovimientos);
      console.log(element.nombreCripto)
      console.log(this.labels)
    });
    var myChart = new Chart("myChart", {
      type: 'bar',
      data: {
          labels: this.labels,
          datasets: [{
              label: '# of Votes',
              data: this.data,
              backgroundColor: [
                  'rgba(255, 99, 132, 0.2)',
                  'rgba(54, 162, 235, 0.2)',
                  'rgba(255, 206, 86, 0.2)',
                  'rgba(75, 192, 192, 0.2)',
                  'rgba(153, 102, 255, 0.2)',
                  'rgba(255, 159, 64, 0.2)'
              ],
              borderColor: [
                  'rgba(255, 99, 132, 1)',
                  'rgba(54, 162, 235, 1)',
                  'rgba(255, 206, 86, 1)',
                  'rgba(75, 192, 192, 1)',
                  'rgba(153, 102, 255, 1)',
                  'rgba(255, 159, 64, 1)'
              ],
              borderWidth: 1
          }]
      },
      options: {
          scales: {
              y: {
                  beginAtZero: true
              }
          }
      }
  });
  }
}
