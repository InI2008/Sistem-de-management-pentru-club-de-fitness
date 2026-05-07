CREATE DATABASE fitness_club_management;
-- Selecteaza baza de date `fitness_club_management`, apoi ruleaza restul scriptului.

CREATE TABLE roles (
    role_id BIGSERIAL PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE users (
    user_id BIGSERIAL PRIMARY KEY,
    role_id BIGINT NOT NULL REFERENCES roles(role_id),
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    phone VARCHAR(20),
    password_hash VARCHAR(255) NOT NULL,
    date_of_birth DATE,
    gender VARCHAR(20),
    emergency_contact_name VARCHAR(120),
    emergency_contact_phone VARCHAR(20),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE membership_plans (
    plan_id BIGSERIAL PRIMARY KEY,
    plan_name VARCHAR(100) NOT NULL UNIQUE,
    duration_months INT NOT NULL CHECK (duration_months > 0),
    price DECIMAL(10,2) NOT NULL CHECK (price >= 0),
    sessions_per_week INT,
    includes_personal_training BOOLEAN NOT NULL DEFAULT FALSE,
    description TEXT,
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE memberships (
    membership_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL REFERENCES users(user_id),
    plan_id BIGINT NOT NULL REFERENCES membership_plans(plan_id),
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    status VARCHAR(30) NOT NULL CHECK (status IN ('active', 'expired', 'frozen', 'cancelled')),
    auto_renew BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT membership_dates_chk CHECK (end_date >= start_date)
);

CREATE TABLE trainers (
    trainer_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL UNIQUE REFERENCES users(user_id),
    specialization VARCHAR(120) NOT NULL,
    biography TEXT,
    hire_date DATE NOT NULL,
    experience_years INT NOT NULL DEFAULT 0 CHECK (experience_years >= 0),
    rating DECIMAL(3,2) DEFAULT 0 CHECK (rating >= 0 AND rating <= 5)
);

CREATE TABLE rooms (
    room_id BIGSERIAL PRIMARY KEY,
    room_name VARCHAR(100) NOT NULL UNIQUE,
    capacity INT NOT NULL CHECK (capacity > 0),
    location_note VARCHAR(255)
);

CREATE TABLE class_categories (
    category_id BIGSERIAL PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE workout_classes (
    class_id BIGSERIAL PRIMARY KEY,
    category_id BIGINT NOT NULL REFERENCES class_categories(category_id),
    trainer_id BIGINT NOT NULL REFERENCES trainers(trainer_id),
    room_id BIGINT NOT NULL REFERENCES rooms(room_id),
    class_name VARCHAR(120) NOT NULL,
    difficulty_level VARCHAR(20) NOT NULL CHECK (difficulty_level IN ('beginner', 'intermediate', 'advanced')),
    duration_minutes INT NOT NULL CHECK (duration_minutes > 0),
    max_capacity INT NOT NULL CHECK (max_capacity > 0),
    description TEXT,
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE class_schedules (
    schedule_id BIGSERIAL PRIMARY KEY,
    class_id BIGINT NOT NULL REFERENCES workout_classes(class_id),
    start_at TIMESTAMP NOT NULL,
    end_at TIMESTAMP NOT NULL,
    booking_deadline TIMESTAMP,
    status VARCHAR(20) NOT NULL DEFAULT 'scheduled' CHECK (status IN ('scheduled', 'completed', 'cancelled')),
    CONSTRAINT schedule_time_chk CHECK (end_at > start_at)
);

CREATE TABLE class_bookings (
    booking_id BIGSERIAL PRIMARY KEY,
    schedule_id BIGINT NOT NULL REFERENCES class_schedules(schedule_id),
    user_id BIGINT NOT NULL REFERENCES users(user_id),
    booking_status VARCHAR(20) NOT NULL DEFAULT 'booked' CHECK (booking_status IN ('booked', 'cancelled', 'attended', 'no_show')),
    booked_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (schedule_id, user_id)
);

CREATE TABLE payments (
    payment_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL REFERENCES users(user_id),
    membership_id BIGINT REFERENCES memberships(membership_id),
    amount DECIMAL(10,2) NOT NULL CHECK (amount >= 0),
    currency CHAR(3) NOT NULL DEFAULT 'EUR',
    payment_method VARCHAR(30) NOT NULL CHECK (payment_method IN ('cash', 'card', 'transfer', 'online')),
    payment_status VARCHAR(20) NOT NULL CHECK (payment_status IN ('pending', 'paid', 'failed', 'refunded')),
    paid_at TIMESTAMP,
    transaction_reference VARCHAR(120) UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE attendance (
    attendance_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL REFERENCES users(user_id),
    schedule_id BIGINT REFERENCES class_schedules(schedule_id),
    check_in_time TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    check_out_time TIMESTAMP,
    attendance_source VARCHAR(30) NOT NULL DEFAULT 'front_desk' CHECK (attendance_source IN ('front_desk', 'mobile_app', 'qr_code'))
);

CREATE TABLE equipment (
    equipment_id BIGSERIAL PRIMARY KEY,
    equipment_name VARCHAR(120) NOT NULL,
    serial_number VARCHAR(100) UNIQUE,
    purchase_date DATE,
    status VARCHAR(20) NOT NULL DEFAULT 'active' CHECK (status IN ('active', 'maintenance', 'retired')),
    room_id BIGINT REFERENCES rooms(room_id)
);

CREATE TABLE maintenance_requests (
    request_id BIGSERIAL PRIMARY KEY,
    equipment_id BIGINT NOT NULL REFERENCES equipment(equipment_id),
    reported_by BIGINT REFERENCES users(user_id),
    issue_title VARCHAR(150) NOT NULL,
    issue_description TEXT NOT NULL,
    priority VARCHAR(20) NOT NULL DEFAULT 'medium' CHECK (priority IN ('low', 'medium', 'high')),
    request_status VARCHAR(20) NOT NULL DEFAULT 'open' CHECK (request_status IN ('open', 'in_progress', 'resolved')),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    resolved_at TIMESTAMP
);

CREATE TABLE announcements (
    announcement_id BIGSERIAL PRIMARY KEY,
    created_by BIGINT NOT NULL REFERENCES users(user_id),
    title VARCHAR(150) NOT NULL,
    message TEXT NOT NULL,
    visible_from TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    visible_until TIMESTAMP,
    audience VARCHAR(20) NOT NULL DEFAULT 'all' CHECK (audience IN ('all', 'members', 'trainers'))
);

CREATE TABLE support_tickets (
    ticket_id BIGSERIAL PRIMARY KEY,
    user_id BIGINT NOT NULL REFERENCES users(user_id),
    subject VARCHAR(150) NOT NULL,
    message TEXT NOT NULL,
    ticket_status VARCHAR(20) NOT NULL DEFAULT 'open' CHECK (ticket_status IN ('open', 'answered', 'closed')),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_users_role_id ON users(role_id);
CREATE INDEX idx_memberships_user_id ON memberships(user_id);
CREATE INDEX idx_memberships_plan_id ON memberships(plan_id);
CREATE INDEX idx_workout_classes_trainer_id ON workout_classes(trainer_id);
CREATE INDEX idx_workout_classes_room_id ON workout_classes(room_id);
CREATE INDEX idx_class_schedules_class_id ON class_schedules(class_id);
CREATE INDEX idx_class_schedules_start_at ON class_schedules(start_at);
CREATE INDEX idx_class_bookings_user_id ON class_bookings(user_id);
CREATE INDEX idx_payments_user_id ON payments(user_id);
CREATE INDEX idx_attendance_user_id ON attendance(user_id);
CREATE INDEX idx_equipment_room_id ON equipment(room_id);
CREATE INDEX idx_maintenance_requests_equipment_id ON maintenance_requests(equipment_id);
CREATE INDEX idx_support_tickets_user_id ON support_tickets(user_id);

INSERT INTO roles (role_name, description) VALUES
('admin', 'Administratorul clubului'),
('member', 'Utilizatorul final al aplicatiei'),
('trainer', 'Antrenorul clubului');
