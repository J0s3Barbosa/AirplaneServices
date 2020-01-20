import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AirplaneDetailComponent } from './airplane-detail.component';

describe('AirplaneDetailComponent', () => {
  let component: AirplaneDetailComponent;
  let fixture: ComponentFixture<AirplaneDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AirplaneDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AirplaneDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
