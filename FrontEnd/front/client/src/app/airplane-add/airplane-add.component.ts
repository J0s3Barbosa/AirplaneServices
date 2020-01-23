import { Component, OnInit, Input } from '@angular/core';

import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../api.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators, FormArray, AbstractControl } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { AirPlaneModel } from '../Shared/AirPlaneModel';

@Component({
  selector: 'app-airplane-add',
  templateUrl: './airplane-add.component.html',
  styleUrls: ['./airplane-add.component.css']
})
export class AirplaneAddComponent implements OnInit {
  dataAirPlaneModelModel: AirPlaneModel[] = [];

  addForm: FormGroup;
  isLoadingResults = true;

  constructor(private router: Router, private api: ApiService, private formBuilder: FormBuilder,
    private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.api.getAirPlaneModels()
      .subscribe(res => {
        this.dataAirPlaneModelModel = res;
        console.log(this.dataAirPlaneModelModel);
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      })

    this.addForm = this.formBuilder.group({
      code: ['', Validators.required],
      model: this.formBuilder.group({
        id: '',
      }),
      numberOfPassengers: new FormControl('', [
        Validators.required,
        Validators.pattern("^[0-9]*$"),
        Validators.minLength(1),
      ])
    });

  }

  onFormSubmit() {
    this.isLoadingResults = true;

    console.log('this.addForm.value');
    console.log(this.addForm.value);

    this.api.postAirplane(this.addForm.value)
      .subscribe(data => {
        this.router.navigate(['/']);
      }
        , (err) => {
         
            console.error("an error occurred here broo");
            console.log(err.status);
            console.log(err.headers);
            console.log(err.error);
            console.log(err.error.title);
            console.log(err.error.errors);
            for (var i in err.error.errors) {
              if (err.error.errors.hasOwnProperty(i)) {
                  console.log(err.error.errors[i].map(
                    function (item) {
                      return item
                    }
                  )
                  );

              }
            }

        }
      );

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
