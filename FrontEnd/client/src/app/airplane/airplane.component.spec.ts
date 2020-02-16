import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AirplaneComponent } from './airplane.component';

describe('AirplaneComponent', () => {
  let component: AirplaneComponent;
  let fixture: ComponentFixture<AirplaneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AirplaneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AirplaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
