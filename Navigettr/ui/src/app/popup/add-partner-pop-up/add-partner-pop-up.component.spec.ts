import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPartnerPopUpComponent } from './add-partner-pop-up.component';

describe('AddPartnerPopUpComponent', () => {
  let component: AddPartnerPopUpComponent;
  let fixture: ComponentFixture<AddPartnerPopUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPartnerPopUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPartnerPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
