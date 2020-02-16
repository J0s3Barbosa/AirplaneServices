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

  airPlaneForm: FormGroup;

  isLoadingResults = false;
  matcher = new MyErrorStateMatcher();


  constructor(private router: Router, private route: ActivatedRoute, private api: ApiService, private formBuilder: FormBuilder) {

  }

  ngOnInit() {
    this.getAirplaneDetails(this.route.snapshot.params['id']);
    this.createForm();
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

  createForm() {
    this.airPlaneForm = this.formBuilder.group({
      code: ['', Validators.required],
      model: this.formBuilder.group({
        id: '',
        name: ''
      }),
      numberOfPassengers: ['', Validators.required]
    });
  }

  onFormSubmit(airPlaneForm: NgForm) {
    this.isLoadingResults = true;

    this.api.updateAirplane(this.route.snapshot.params['id'], airPlaneForm)
      .subscribe(res => {
        this.isLoadingResults = false;
        this.airplaneDetails(res.id);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
      );
  }

  airplaneDetails(id) {
    this.router.navigate(['/airplane-details', id]);
  }

  getAirplaneDetails(id: string) {
    this.api.getAirplane(id)
      .subscribe(data => {
        this.isLoadingResults = true;

        this.airPlaneForm.setValue({
          code: data.code,
          model: data.model,
          numberOfPassengers: data.numberOfPassengers,
        });
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
