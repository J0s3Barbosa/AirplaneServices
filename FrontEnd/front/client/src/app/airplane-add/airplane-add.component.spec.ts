import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AirplaneAddComponent } from './airplane-add.component';

describe('AirplaneAddComponent', () => {
  let component: AirplaneAddComponent;
  let fixture: ComponentFixture<AirplaneAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AirplaneAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AirplaneAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
