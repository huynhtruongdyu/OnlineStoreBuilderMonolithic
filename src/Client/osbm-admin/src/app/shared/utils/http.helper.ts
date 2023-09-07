export const objectToQueryString = (obj: any): string => {
  const queryParams = [];

  for (const key in obj) {
    if (obj.hasOwnProperty(key)) {
      const value = obj[key];
      queryParams.push(`${encodeURIComponent(key)}=${encodeURIComponent(value)}`);
    }
  }

  return queryParams.join('&');
}
