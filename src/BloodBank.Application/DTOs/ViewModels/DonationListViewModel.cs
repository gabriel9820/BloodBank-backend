using BloodBank.Core.Entities;

namespace BloodBank.Application.DTOs.ViewModels;

public class DonationListViewModel(
    int id,
    DateTime donationDate,
    int quantityML,
    DonorListViewModel donor)
{
    public int Id { get; private set; } = id;
    public DateTime DonationDate { get; private set; } = donationDate;
    public int QuantityML { get; private set; } = quantityML;
    public DonorListViewModel Donor { get; private set; } = donor;
}

public static partial class DonationExtensions
{
    public static DonationListViewModel ToListViewModel(this Donation donation)
    {
        return new DonationListViewModel(
            id: donation.Id,
            donationDate: donation.DonationDate,
            quantityML: donation.QuantityML,
            donor: donation.Donor.ToListViewModel()
        );
    }
}
