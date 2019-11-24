import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BulkImportLocationPopUpComponent } from './bulk-import-location-pop-up.component';

describe('BulkImportLocationPopUpComponent', () => {
  let component: BulkImportLocationPopUpComponent;
  let fixture: ComponentFixture<BulkImportLocationPopUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BulkImportLocationPopUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkImportLocationPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
