import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { AirPlane } from '../Shared/AirPlane';

@Component({
  selector: 'app-airplane',
  templateUrl: './airplane.component.html',
  styleUrls: ['./airplane.component.css']
})
export class AirplaneComponent implements OnInit {

  displayedColumns: string[] = ['id', 'code', 'model', 'numberOfPassengers', 'creationDate'];
  data: AirPlane[] = [];
  isLoadingResults = true;


  constructor(private api: ApiService) { }
 
  ngOnInit() {
    this.api.getAirplanes()
      .subscribe(res => {
        this.data = res;
        console.log(this.data);
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }


}