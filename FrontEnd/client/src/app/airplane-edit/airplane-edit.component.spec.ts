import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AirplaneEditComponent } from './airplane-edit.component';

describe('AirplaneEditComponent', () => {
  let component: AirplaneEditComponent;
  let fixture: ComponentFixture<AirplaneEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AirplaneEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AirplaneEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
