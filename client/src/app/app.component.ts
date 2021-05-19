import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { ChallengeService } from './services/challenge.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';
  form: FormGroup;
  public loading: boolean = false;
  public submitted: boolean = false;
  error: string;
  defaultCode: string = 'Console.WriteLine("Hello");';
  output: string;

  constructor(
    private formBuilder: FormBuilder,
    private challengeService : ChallengeService
  ) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
        playerName: ['', Validators.required],
        solutionCode: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
        return;
    }

    this.loading = true;
    
    this.challengeService.submitTask(this.form.value)
      .pipe(first())
      .subscribe({
          next: challlenge => {
              this.loading = false;
              this.error = null;
              this.output = challlenge.output;
          },
          error: error => {
              this.loading = false;
              this.output = null;
              this.error = error;
          }
      });
  }
}

