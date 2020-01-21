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

  airPlaneModelForm: FormGroup;
  code = '';
  model = null;
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
      model: ['', Validators.required ],
      numberOfPassengers: ['', Validators.required ]
      });
    }


  ngOnInit() {
    this.route.params.subscribe(params => {
      this.api
      .putAirPlane(params['id'], this.airPlaneModelForm);
  });


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
          model: data.model,
          numberOfPassengers: data.numberOfPassengers,
        });
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
