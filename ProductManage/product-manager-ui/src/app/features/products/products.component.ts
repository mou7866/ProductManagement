import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductService } from './products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  imports: [CommonModule, ReactiveFormsModule]
})

export class ProductsComponent implements OnInit {
  products$!: Observable<Product[]>;
  categories$!: Observable<{ id: string; name: string }[]>;
  loading$!: Observable<boolean>;
  form: FormGroup;
  showForm: boolean = false;
  isEditMode: boolean = false;
  currentProductId: string | null = null;

  constructor(private productService: ProductService, private fb: FormBuilder) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: [0, [Validators.required, Validators.min(0)]],
      categoryId: ['', Validators.required],
      status: ['', Validators.required],
      stockQuantity: [0, [Validators.required, Validators.min(0)]],
      imageUrl: ['']
    });
  }

  ngOnInit(): void {
    this.products$ = this.productService.products$;
    this.categories$ = this.productService.categories$;
    this.loading$ = this.productService.loading$;

    // Fetch initial data
    this.productService.fetchProducts();
    this.productService.fetchCategories();
  }

  onAdd(): void {
    this.isEditMode = false;
    this.showForm = true;
    this.form.reset({
      name: '',
      description: '',
      price: 0,
      categoryId: '',
      status: '',
      stockQuantity: 0,
      imageUrl: ''
    });
  }

  onSave(): void {
    if (this.form.invalid) return;

    const productData = this.form.getRawValue();

    if (this.isEditMode && this.currentProductId) {
      // Update product
      this.productService.updateProduct(this.currentProductId, productData).subscribe({
        next: () => {
          this.resetForm();
          this.productService.fetchProducts();
        },
        error: (err) => {
          console.error('Error updating product:', err);
          alert('Failed to update product. Please try again.');
        }
      });
    } else {
      // Create new product
      this.productService.createProduct(productData).subscribe({
        next: () => {
          this.resetForm();
          this.productService.fetchProducts();
        },
        error: (err) => {
          console.error('Error creating product:', err);
          alert('Failed to create product. Please try again.');
        }
      });
    }
  }

  onEdit(product: Product): void {
    this.isEditMode = true;
    this.showForm = true;
    this.currentProductId = product.id;
    this.form.patchValue(product);
  }

  onDelete(productId: string): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(productId).subscribe({
        next: () => {
          this.productService.fetchProducts();
          console.log('Product deleted successfully');
        },
        error: (err) => {
          console.error('Error deleting product:', err);
          alert('Failed to delete product. Please try again.');
        }
      });
    }
  }

  onCancel(): void {
    this.resetForm();
  }

  private resetForm(): void {
    this.showForm = false;
    this.form.reset();
    this.isEditMode = false;
    this.currentProductId = null;
  }
}