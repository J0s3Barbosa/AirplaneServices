import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../api.service';
import { AirPlane } from '../Shared/AirPlane';
import { AirPlaneModel } from '../Shared/AirPlaneModel';

@Component({
  selector: 'app-airplane-detail',
  templateUrl: './airplane-detail.component.html',
  styleUrls: ['./airplane-detail.component.css']
})
export class AirplaneDetailComponent implements OnInit {
 
  airPlaneModel: AirPlane = {
    id: '',
    code: '',
    model: new AirPlaneModel,
    numberOfPassengers: null,
    creationDate: null ,
  };
  isLoadingResults = true;
 
  getAirplaneDetails(id: string) {
    this.api.getAirplane(id)
      .subscribe(data => {
        this.airPlaneModel = data;
        console.log(this.airPlaneModel);
        this.isLoadingResults = false;
      });
  }
  deleteAirplane(id: string) {
    this.isLoadingResults = true;
    this.api.deleteAirplane(id)
      .subscribe(res => {
          this.isLoadingResults = false;
          this.router.navigate(['/airplane']);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        }
      );
  }
  
  constructor(private route: ActivatedRoute, private api: ApiService, private router: Router) { }

  ngOnInit() {

    this.getAirplaneDetails(this.route.snapshot.params['id']);
  }

}
