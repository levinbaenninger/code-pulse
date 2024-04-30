import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPostService } from '../../../services/blog-post.service';

@Component({
  selector: 'app-blog-post-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './blog-post-form.component.html',
  styleUrl: './blog-post-form.component.scss',
})
export class BlogPostFormComponent implements OnInit, OnDestroy {
  blogPostForm!: FormGroup;
  private idSubscription?: Subscription;
  private createSubscription?: Subscription;
  private deleteSubscription?: Subscription;
  private updateSubscription?: Subscription;
  id?: string;

  constructor(
    private formBuilder: FormBuilder,
    private blogPostService: BlogPostService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.blogPostForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      urlHandle: ['', Validators.required],
      content: ['', Validators.required],
      featuredImageUrl: ['', Validators.required],
      datePublished: ['', Validators.required],
      author: ['', Validators.required],
      isVisible: [true, Validators.required],
    });

    this.idSubscription = this.activatedRoute.params.subscribe((params) => {
      this.id = params['id'];

      if (this.id) {
        this.blogPostService.getBlogPost(this.id).subscribe((blogPost) => {
          this.blogPostForm.setValue({
            title: blogPost.title,
            description: blogPost.description,
            urlHandle: blogPost.urlHandle,
            content: blogPost.content,
            featuredImageUrl: blogPost.featuredImageUrl,
            datePublished: blogPost.datePublished,
            author: blogPost.author,
            isVisible: blogPost.isVisible,
          });
        });
      }
    });
  }

  ngOnDestroy(): void {
    this.idSubscription?.unsubscribe();
    this.createSubscription?.unsubscribe();
    this.deleteSubscription?.unsubscribe();
    this.updateSubscription?.unsubscribe();
  }

  onFormSubmit(): void {
    if (this.blogPostForm.invalid) {
      return;
    }

    if (this.id) {
      this.updateSubscription = this.blogPostService
        .updateBlogPost(this.id, this.blogPostForm.value)
        .subscribe(() => this.router.navigate(['/admin/blog-posts']));
      return;
    }

    this.createSubscription = this.blogPostService
      .createBlogPost(this.blogPostForm.value)
      .subscribe(() => {
        this.router.navigate(['/admin/blog-posts']);
      });
  }

  onReset(): void {
    this.blogPostForm.reset();
  }

  onDelete(): void {
    if (!this.id) {
      return;
    }

    this.deleteSubscription = this.blogPostService
      .deleteBlogPost(this.id)
      .subscribe(() => {
        this.router.navigate(['/admin/blog-posts']);
      });
  }
}
