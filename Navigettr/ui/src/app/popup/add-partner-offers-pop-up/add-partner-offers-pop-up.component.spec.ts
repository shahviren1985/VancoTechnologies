import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPartnerOffersPopUpComponent } from './add-partner-offers-pop-up.component';

describe('AddPartnerOffersPopUpComponent', () => {
  let component: AddPartnerOffersPopUpComponent;
  let fixture: ComponentFixture<AddPartnerOffersPopUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPartnerOffersPopUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPartnerOffersPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
