import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { AirPlaneModel } from '../Shared/AirPlaneModel';


@Component({
  selector: 'app-airplane-edit',
  templateUrl: './airplane-edit.component.html',
  styleUrls: ['./airplane-edit.component.css']
})
export class AirplaneEditComponent implements OnInit {
  dataAirPlaneModel: AirPlaneModel[] = [];

  airPlaneModelForm: FormGroup;
  code = '';
  modelId = null;
  numberOfPassengers = null;

  isLoadingResults = false;
  matcher = new MyErrorStateMatcher();


  constructor(private router: Router, private route: ActivatedRoute, private api: ApiService, private formBuilder: FormBuilder) { 
    this.createForm();
    this.getAirplaneDetails(this.route.snapshot.params['id']);
  }
  createForm() {
    this.airPlaneModelForm = this.formBuilder.group({
      code: ['', Validators.required ],
      modelId: ['', Validators.required ],
      numberOfPassengers: ['', Validators.required ]
      });
    }


  ngOnInit() {
   
  this.api.getAirPlaneModels()
  .subscribe(res => {
    this.dataAirPlaneModel = res;
    console.log(this.dataAirPlaneModel);
    this.isLoadingResults = false;
  }, err => {
    console.log(err);
    this.isLoadingResults = false;
  })

  }

  onFormSubmit(airPlaneModelForm : NgForm) {
    this.isLoadingResults = true;
    this.api.updateAirplane(this.route.snapshot.params['id'], airPlaneModelForm)
      .subscribe(res => {
          this.isLoadingResults = false;
          this.airplaneDetails();
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        }
      );
  }
  
  airplaneDetails() {
    this.router.navigate(['/airplane-details', this.route.snapshot.params['id']]);
  }

  getAirplaneDetails(id: string) {
    this.api.getAirplane(id)
      .subscribe(data => {
        this.isLoadingResults = true;
        this.airPlaneModelForm.setValue({
          code: data.code,
          modelId: data.model.id,
          numberOfPassengers: data.numberOfPassengers,
        });
        console.log('this.airPlaneModelForm');
        console.log(this.airPlaneModelForm);
        this.isLoadingResults = false;
      });
  }

  
}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
