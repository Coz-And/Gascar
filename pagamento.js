<script src="https://js.stripe.com/v3/"></script>
<script>
const stripe = Stripe('pk_test_TUO_PUBLISHABLE_KEY');

document.getElementById('payment-form').addEventListener('submit', async e => {
    e.preventDefault();

   
    const {error, paymentMethod} = await stripe.createPaymentMethod({
        type: 'card',
        card: {
            number: document.getElementById('numero_carta').value.replace(/\s/g,''),
            exp_month: document.getElementById('scadenza').value.split('/')[0],
            exp_year:  '20' + document.getElementById('scadenza').value.split('/')[1],
            cvc: document.getElementById('cvc').value,
        },
        billing_details: {
            name: `${document.getElementById('nome').value} ${document.getElementById('cognome').value}`,
        },
    });

    if (error) {
        alert(error.message);
        return;
    }

    // 2️⃣ Invia il paymentMethod.id al tuo back‑end
    const response = await fetch('/process-payment', {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({
            payment_method_id: paymentMethod.id,
            tariffa: document.getElementById('tariffa').value,
            // altri dati di fatturazione…
        })
    });

    const result = await response.json();
    if (result.success) {
        window.location.href = '/conferma';
    } else {
        alert('Pagamento fallito: ' + result.error);
    }
});
</script>