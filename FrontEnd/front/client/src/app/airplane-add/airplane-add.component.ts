import { Component, OnInit, Input } from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { AirPlaneAddModel } from '../Shared/AirPlaneAddModel';
import { ErrorStateMatcher } from '@angular/material/core';
import { AirPlaneModelModel } from '../Shared/AirPlaneModelModel';

@Component({
  selector: 'app-airplane-add',
  templateUrl: './airplane-add.component.html',
  styleUrls: ['./airplane-add.component.css']
})
export class AirplaneAddComponent implements OnInit {
  dataAirPlaneModelModel: AirPlaneModelModel[] = [];
  orders = [];

  airPlaneAddModelForm: FormGroup;
  code = '';
  model = '';
  numberOfPassengers = '';
  isLoadingResults = true;

  constructor(private router: Router, private api: ApiService, private formBuilder: FormBuilder,
    private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.airPlaneAddModelForm = this.formBuilder.group({
      'code': [null, Validators.required],
      'model': [null, Validators.required],
      'numberOfPassengers': [null, Validators.required],

    });
    this.api.getAirPlaneModelModels()
      .subscribe(res => {
        this.dataAirPlaneModelModel = res;
        console.log(this.dataAirPlaneModelModel);
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      })
  }

  onFormSubmit(form: NgForm) {
    this.isLoadingResults = true;

    console.log('form');
    console.log(form);

    this.api.addAirplane(form)
      .subscribe(response => {
      console.log(response);
        this.isLoadingResults = false;
        this.router.navigate(['/']);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      });
    this.isLoadingResults = false;
  }

}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
