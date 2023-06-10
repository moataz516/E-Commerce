import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductActionComponent } from './product-action.component';

describe('ProductActionComponent', () => {
  let component: ProductActionComponent;
  let fixture: ComponentFixture<ProductActionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductActionComponent]
    });
    fixture = TestBed.createComponent(ProductActionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
