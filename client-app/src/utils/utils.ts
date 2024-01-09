export const isMoreThan30DaysAgo = (lastModifiedDate: string): boolean => {
    const lastModified = new Date(lastModifiedDate);
    const currentDate = new Date();
    const timeDifference = currentDate.getTime() - lastModified.getTime();
    const daysDifference = timeDifference / (1000 * 60 * 60 * 24);

    return daysDifference > 30;
};