import 'package:flutter/material.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  // Controllers
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();

  // Form & States
  final _formKey = GlobalKey<FormState>();
  bool _isPasswordVisible = false;
  bool _isLoading = false;

  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  Future<void> _onLogin() async {
    FocusScope.of(context).unfocus();
    if (!_formKey.currentState!.validate()) return;
    setState(() => _isLoading = true);
    // Simüle giriş işlemi
    await Future.delayed(const Duration(milliseconds: 1200));
    if (!mounted) return;
    setState(() => _isLoading = false);

    // Demo: Başarılı girişte snack bar
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(content: Text('Giriş başarılı!')),
    );
  }

  String? _validateEmail(String? value) {
    final email = value?.trim() ?? '';
    if (email.isEmpty) return 'E-posta gerekli';
    // Basit e-posta doğrulaması
    final regex = RegExp(r'^.+@.+\..+$');
    if (!regex.hasMatch(email)) return 'Geçerli bir e-posta girin';
    return null;
  }

  String? _validatePassword(String? value) {
    final pass = value ?? '';
    if (pass.isEmpty) return 'Şifre gerekli';
    if (pass.length < 6) return 'En az 6 karakter';
    return null;
  }

  @override
  Widget build(BuildContext context) {
    final size = MediaQuery.of(context).size;
    final isSmall = size.height < 700;

    return GestureDetector(
      onTap: () => FocusScope.of(context).unfocus(),
      child: Scaffold(
        resizeToAvoidBottomInset: true,
        body: SafeArea(
          child: Center(
            child: SingleChildScrollView(
              padding: EdgeInsets.fromLTRB(24, isSmall ? 24 : 40, 24, 24 + MediaQuery.of(context).viewInsets.bottom),
              child: ConstrainedBox(
                constraints: const BoxConstraints(maxWidth: 520),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.stretch,
                  children: [
                    SizedBox(height: isSmall ? 8 : 16),

                    // Marka Başlık + Slogan (daha sade)
                    _BrandHeader(isCompact: isSmall),
                    SizedBox(height: isSmall ? 16 : 24),

                    // Sade kart
                    _GlassCard(
                      child: Padding(
                        padding: const EdgeInsets.all(20.0),
                        child: Form(
                          key: _formKey,
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.stretch,
                            children: [
                              Text(
                                'Giriş Yap',
                                style: Theme.of(context).textTheme.titleLarge?.copyWith(
                                      fontWeight: FontWeight.w700,
                                    ),
                              ),
                              const SizedBox(height: 6),
                              Text(
                                'BeeFocus ile odaklan ve tamamla.',
                                style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                                      color: Theme.of(context).textTheme.bodyMedium?.color?.withOpacity(0.7),
                                    ),
                              ),
                              const SizedBox(height: 20),

                              // E-posta
                              _GlassField(
                                controller: _emailController,
                                label: 'E-posta',
                                hint: 'ornek@mail.com',
                                icon: Icons.email_outlined,
                                keyboardType: TextInputType.emailAddress,
                                validator: _validateEmail,
                              ),
                              const SizedBox(height: 14),

                              // Şifre
                              _GlassField(
                                controller: _passwordController,
                                label: 'Şifre',
                                hint: '••••••••',
                                icon: Icons.lock_outline,
                                obscureText: !_isPasswordVisible,
                                validator: _validatePassword,
                                trailing: IconButton(
                                  onPressed: () => setState(() => _isPasswordVisible = !_isPasswordVisible),
                                  icon: Icon(
                                    _isPasswordVisible ? Icons.visibility_off : Icons.visibility,
                                  ),
                                ),
                              ),

                              const SizedBox(height: 10),
                              Align(
                                alignment: Alignment.centerRight,
                                child: TextButton(
                                  onPressed: () {},
                                  child: const Text('Şifremi unuttum'),
                                ),
                              ),

                              const SizedBox(height: 4),
                              _GradientButton(
                                onTap: _isLoading ? null : _onLogin,
                                child: AnimatedSwitcher(
                                  duration: const Duration(milliseconds: 250),
                                  child: _isLoading
                                      ? const SizedBox(
                                          height: 22,
                                          width: 22,
                                          child: CircularProgressIndicator(strokeWidth: 2.4),
                                        )
                                      : const Text(
                                          'Giriş Yap',
                                          style: TextStyle(
                                            fontSize: 16,
                                            fontWeight: FontWeight.w700,
                                            letterSpacing: 0.3,
                                          ),
                                        ),
                                ),
                              ),

                              const SizedBox(height: 14),
                              Row(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: [
                                  Text(
                                    'Hesabın yok mu? ',
                                    style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                                          color: Theme.of(context).textTheme.bodyMedium?.color?.withOpacity(0.7),
                                        ),
                                  ),
                                  TextButton(
                                    onPressed: () {
                                      // TODO: Kayıt ekranına yönlendir
                                    },
                                    child: const Text('Hemen Kayıt Ol'),
                                  )
                                ],
                              )
                            ],
                          ),
                        ),
                      ),
                    ),

                    SizedBox(height: isSmall ? 16 : 28),

                    // Alt küçük not ya da telif
                    Center(
                      child: Text(
                        'BeeFocus · odak ve verimlilik için',
                        style: Theme.of(context).textTheme.bodySmall?.copyWith(
                              color: Theme.of(context).textTheme.bodySmall?.color?.withOpacity(0.6),
                            ),
                      ),
                    ),
                    const SizedBox(height: 24),
                  ],
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}

class _BrandHeader extends StatelessWidget {
  final bool isCompact;
  const _BrandHeader({required this.isCompact});

  @override
  Widget build(BuildContext context) {
    final titleStyle = Theme.of(context).textTheme.headlineMedium?.copyWith(
          fontWeight: FontWeight.w800,
        );

    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        Center(
          child: CircleAvatar(
            radius: isCompact ? 24 : 28,
            backgroundColor: Colors.amber.shade400,
            child: Icon(
              Icons.bolt_rounded,
              color: Colors.black87,
              size: isCompact ? 22 : 24,
            ),
          ),
        ),
        const SizedBox(height: 14),
        Text('BeeFocus', style: titleStyle, textAlign: TextAlign.center),
        const SizedBox(height: 4),
        Text(
          'Odaklan ve tamamla',
          style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                color: Theme.of(context).textTheme.bodyMedium?.color?.withOpacity(0.7),
              ),
          textAlign: TextAlign.center,
        ),
      ],
    );
  }
}

class _GlassCard extends StatelessWidget {
  final Widget child;
  const _GlassCard({required this.child});

  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: Theme.of(context).brightness == Brightness.dark ? 0 : 1,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(16),
        side: BorderSide(
          color: Theme.of(context).dividerColor.withOpacity(0.2),
        ),
      ),
      clipBehavior: Clip.antiAlias,
      child: child,
    );
  }
}

class _GlassField extends StatelessWidget {
  final TextEditingController controller;
  final String label;
  final String hint;
  final IconData icon;
  final bool obscureText;
  final TextInputType? keyboardType;
  final String? Function(String?)? validator;
  final Widget? trailing;

  const _GlassField({
    required this.controller,
    required this.label,
    required this.hint,
    required this.icon,
    this.obscureText = false,
    this.keyboardType,
    this.validator,
    this.trailing,
  });

  @override
  Widget build(BuildContext context) {
    final baseBorder = OutlineInputBorder(
      borderRadius: BorderRadius.circular(12),
    );

    return TextFormField(
      controller: controller,
      validator: validator,
      keyboardType: keyboardType,
      obscureText: obscureText,
      decoration: InputDecoration(
        labelText: label,
        hintText: hint,
        prefixIcon: Icon(icon),
        suffixIcon: trailing,
        border: baseBorder,
        enabledBorder: baseBorder.copyWith(
          borderSide: BorderSide(color: Theme.of(context).dividerColor.withOpacity(0.4)),
        ),
        focusedBorder: baseBorder.copyWith(
          borderSide: BorderSide(color: Theme.of(context).colorScheme.primary, width: 1.6),
        ),
        errorBorder: baseBorder.copyWith(
          borderSide: BorderSide(color: Theme.of(context).colorScheme.error),
        ),
        focusedErrorBorder: baseBorder.copyWith(
          borderSide: BorderSide(color: Theme.of(context).colorScheme.error),
        ),
        contentPadding: const EdgeInsets.symmetric(horizontal: 14, vertical: 16),
      ),
    );
  }
}

class _GradientButton extends StatelessWidget {
  final VoidCallback? onTap;
  final Widget child;
  const _GradientButton({required this.onTap, required this.child});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 52,
      child: ElevatedButton(
        onPressed: onTap,
        style: ElevatedButton.styleFrom(
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
        ),
        child: DefaultTextStyle.merge(
          style: const TextStyle(fontWeight: FontWeight.w700),
          child: child,
        ),
      ),
    );
  }
}
