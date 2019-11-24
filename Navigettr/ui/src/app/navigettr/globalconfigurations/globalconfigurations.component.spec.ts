import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalconfigurationsComponent } from './globalconfigurations.component';

describe('GlobalconfigurationsComponent', () => {
  let component: GlobalconfigurationsComponent;
  let fixture: ComponentFixture<GlobalconfigurationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlobalconfigurationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlobalconfigurationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
