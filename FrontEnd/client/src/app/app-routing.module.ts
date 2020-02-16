import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AirplaneComponent } from './airplane/airplane.component';
import { AirplaneDetailComponent } from './airplane-detail/airplane-detail.component';
import { AirplaneAddComponent } from './airplane-add/airplane-add.component';
import { AirplaneEditComponent } from './airplane-edit/airplane-edit.component';

const routes: Routes = [
  {
    path: 'airplane',
    component: AirplaneComponent,
    data: { title: 'List of Airplanes' }
  },
  {
    path: 'airplane-details/:id',
    component: AirplaneDetailComponent,
    data: { title: 'Airplane Details' }
  },
  {
    path: 'airplane-add',
    component: AirplaneAddComponent,
    data: { title: 'Add Airplane' }
  },
  {
    path: 'airplane-edit/:id',
    component: AirplaneEditComponent,
    data: { title: 'Edit Airplane' }
  },
  { path: '',
    redirectTo: '/airplane',
    pathMatch: 'full'
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
