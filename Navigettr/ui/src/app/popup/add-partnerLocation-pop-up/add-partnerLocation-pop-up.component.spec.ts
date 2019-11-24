import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { AddPartnerLocationPopUpComponent } from './add-partnerLocation-pop-up.component';

describe('AddPartnerLocationPopUpComponent', () => {
  let component: AddPartnerLocationPopUpComponent;
  let fixture: ComponentFixture<AddPartnerLocationPopUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPartnerLocationPopUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPartnerLocationPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
