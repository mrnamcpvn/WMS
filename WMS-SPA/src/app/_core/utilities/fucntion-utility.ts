import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root', })
export class FunctionUtility {
  constructor() { }

  /**
   * Convert date to string format
   * @returns yyyy/MM/dd
   */
  getStringDate(date?: Date) {
    date = date ?? new Date();
    return `${date.getFullYear()}/${date.getMonth() + 1 < 10 ? ('0' + (date.getMonth() + 1)) : (date.getMonth() + 1)}/${date.getDate()}`;
  }

  /**
   * Convert input date to short date string format
   * @param date
   * @returns yyyyMM
   */
  getFullYearAndMonth(date?: Date) {
    date = date ?? new Date();
    return `${date.getFullYear()}${date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : (date.getMonth() + 1)}`;
  }

  /**
   * Convert to short month string format
   * @param month
   * @returns MMM.
   */
  getShortMonth(month: number) {
    const months = ['Jan.', 'Feb.', 'Mar.', 'Apr.', 'May', 'Jun.', 'Jul.', 'Aug.', 'Sep.', 'Oct.', 'Nov.', 'Dec.'];
    return months[month - 1];
  }

  /**
   * Return the first date of current month
   * @returns Date
   */
  getFirstDateOfMonth(date?: Date) {
    date = date ?? new Date();
    return new Date(date.getFullYear() + '/' + (date.getMonth() + 1) + '/' + '01');
  }

  /**
     * Get UTC Date
     * @returns new UTC Date
     */
  getUTCDate(date?: Date) {
    date = date ?? new Date();
    return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds(), date.getMilliseconds()));
  }

  /**
   * Convert input date to date without time
   * @param date
   * @returns Date
   */
  getDefaultTimeUTCDate(date?: Date) {
    date = date ?? new Date();
    return new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate(), 0, 0, 0));
  }
}
