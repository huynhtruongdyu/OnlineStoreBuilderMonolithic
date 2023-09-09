import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollectionsManagementComponent } from './collections-management.component';

describe('CollectionsManagementComponent', () => {
  let component: CollectionsManagementComponent;
  let fixture: ComponentFixture<CollectionsManagementComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CollectionsManagementComponent]
    });
    fixture = TestBed.createComponent(CollectionsManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
