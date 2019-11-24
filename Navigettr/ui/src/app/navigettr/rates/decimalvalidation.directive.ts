import { Directive, ElementRef, HostListener } from "@angular/core";

@Directive({ selector: "[DecimalValidator]" })
export class DecimalValidatorDirective {
  private regex: RegExp = new RegExp(/^\d*\.?\d{0,4}$/g);
  private specialKeys: Array<string> = ["Backspace", "Tab", "End", "Home", "-"];
  private el: HTMLInputElement;

  constructor(private elementRef: ElementRef) {
    this.el = this.elementRef.nativeElement;
  }

  ngOnInit() {}

  @HostListener("keydown", ["$event"])
  onKeyDown(event: KeyboardEvent) {
    // Allow Backspace, tab, end, and home keys
    if (this.specialKeys.indexOf(event.key) !== -1) {
      return;
    }
    let current: string = this.el.value;
    let next: string = current.concat(event.key);
    if (next && !String(next).match(this.regex)) {
      event.preventDefault();
    }
  }
}
