import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustompartnerchargesComponent } from './custompartnercharges.component';

describe('CustompartnerchargesComponent', () => {
  let component: CustompartnerchargesComponent;
  let fixture: ComponentFixture<CustompartnerchargesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustompartnerchargesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustompartnerchargesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
