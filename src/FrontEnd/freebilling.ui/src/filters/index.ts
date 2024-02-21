const language = "en-US";
let moneyFormatter = new Intl.NumberFormat('language', {
  style: 'currency',
  currency: 'USD',
});

export function money(value: number | string) {
  if (typeof value === "number") {
    return moneyFormatter.format(value);
  } else {
    return moneyFormatter.format(Number(value));
  }
}

export function shortDate(value: Date | string | null) {
  if (value) {
    if (value instanceof Date) {
      return value.toLocaleDateString(language);
    } else {
      return new Date(value).toLocaleDateString(language);
    }
  }

  return "";
}