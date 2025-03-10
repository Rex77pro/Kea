import marimo

__generated_with = "0.11.17"
app = marimo.App(width="medium")


@app.cell
def _():
    import marimo as mo
    import matplotlib.pyplot as plt
    import pandas as pd
    import time
    from geopy.geocoders import Nominatim
    return Nominatim, mo, pd, plt, time


@app.cell
def _(pd):
    # Læs CSV-filen med komma som separator
    df = pd.read_csv("DKHousingPricesSample100k.csv", sep=",")
    return (df,)


@app.cell
def _(Nominatim):
    geolocator = Nominatim(user_agent="danske_huspriser")
    return (geolocator,)


@app.cell
def _(df):
    # Udvælg de første 10 rækker og lav en kopi for at undgå SettingWithCopyWarning
    df_subset = df.head(100).copy()
    return (df_subset,)


@app.cell
def _(geolocator, time):
    def geocode_row(row):
        address = row['address']
        zip_code = row['zip_code']
        city = row['city']
        # Kombiner til en fuld adresse – juster evt. formatet efter behov
        full_address = f"{address}, {zip_code} {city}, Danmark"
        try:
            location = geolocator.geocode(full_address)
            time.sleep(1)  # Pause for at overholde API'ets grænse
            if location:
                return location.latitude, location.longitude
        except Exception as e:
            print(f"Fejl ved geokodning af {full_address}: {e}")
        return None, None
    return (geocode_row,)


@app.cell
def _(df_subset, geocode_row, pd):
    df_subset[['lat', 'lon']] = df_subset.apply(lambda row: pd.Series(geocode_row(row)), axis=1)
    df_subset
    return


@app.cell
def _(df_subset, plt):
    fig, ax = plt.subplots(figsize=(10,8))
    scatter = ax.scatter(df_subset['lon'], df_subset['lat'],
                         c=df_subset['purchase_price'], cmap='viridis', s=100)
    return ax, fig, scatter


@app.cell
def _(ax, fig, scatter):
    cbar = fig.colorbar(scatter, ax=ax)
    cbar.set_label("Købspris")
    return (cbar,)


@app.cell
def _(ax):
    ax.set_xlabel("Longitude")
    ax.set_ylabel("Latitude")
    ax.set_title("Scatter Plot af Huspriser")
    return


@app.cell
def _(plt):
    plt.show()
    return


if __name__ == "__main__":
    app.run()
